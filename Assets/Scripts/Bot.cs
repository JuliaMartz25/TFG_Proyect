using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target; //target sera el cop que lo pondremos en unity

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    //Seek es el metodo mas sencillo, basicamente se trata de seguir "algo" por la NavMesh
    void Seek(Vector3 location)
    {
        agent.SetDestination(location); //location en este caso es el cop y necesitamos el "target" como global
    }

    //En seek ponemos como destino la posicion del target, en Flee (huir) ponemos como
    //destino la posicion del target en negativo para que vaya huyendo de esa posicion
    void Flee(Vector3 location)
    {
        Vector3 fleeVector = location - this.transform.position;
        agent.SetDestination(this.transform.position - fleeVector);
    }

    //Perseguir es parecido a Seek pero siguiendo un punto donde predecimos que estara el target unos instantes despues.
    //Para ello necesitamos la posicion, velocidad y dirección del target, además de la velocidad del perseguidor y su distancia al target.
    //Así podemos calcular (predecir) el punto donde se encontrarian si nada cambia.
    void Pursue()
    {
        Vector3 targetDir = target.transform.position - this.transform.position;
       /* float cSpeed = target.GetComponent<Drive>().currentSpeed;*/
        //Hacemos que la posicion del agent sea el origen de coordenadas
        float relativeHeading = Vector3.Angle(this.transform.forward, this.transform.TransformVector(target.transform.forward));
        float toTarget = Vector3.Angle(this.transform.forward, this.transform.TransformVector(targetDir));
        //Si el robber esta en frente el angulo sera mayor de 90, sin embargo, si esta detras sera menor de 90
        //Si el angulo es muy pequeño, necesitamos que solo siga al target
        if ((toTarget > 90 && relativeHeading < 20) /*|| cSpeed < 0.01f*/) //La velocidad
                                                                       //es muy poca (mejor ponerlo asi que
                                                                       //poner 0 por errores de precision que hace que no sea exactamente 0)
        {
            Seek(target.transform.position);
            return;
        }

        //Mirar hacia delante y calcular lo lejos que tenemos que ir para encontrar al target que se mueve
        float lookAhead = targetDir.magnitude / (agent.speed /*+ cSpeed*/);
        Seek(target.transform.position + target.transform.forward * lookAhead); //Si multiplicamos por 5 todavía predice con mas adelanto
    }

    //Evade es practicamente igual a Pursue solo que se debe ir en la direccion contraria
    void Evade()
    {
        Vector3 targetDir = target.transform.position - this.transform.position;
        float lookAhead = targetDir.magnitude / (agent.speed /*+ target.GetComponent<Drive>().currentSpeed*/);
        Flee(target.transform.position + target.transform.forward * lookAhead); //Si multiplicamos por 5 todavía predice con mas adelanto
    }


    //Para Wander necesitamos una var. que recordemos en cada llamada y por lo
    //tanto no puede ser local al metodo.
    Vector3 wanderTarget = Vector3.zero; //Se actualiza cada vez que el agente
										 //tiene un nuevo valor en su posicion
    void Wander()
    {
        //Estos valores son los que tenemos que variar si queremos cambiar el wander
        //behaviour. Podemos hacerlos publicos arriba del todo si queremos que sean
        //accesibles desde el "inspector".
        //IMPORTANTE: Hay que estar muy seguros de que la pos. que estamos calculando
        //existe en nuestro NavMesh, si no tendremos problemas cuando seteemos la
        //"location" ya que el circulo de movimiento del personaje cubrira una zona
        //no navegable por el mismo
        float wanderRadius   = 4f;
        float wanderDistance = 8f;
        float wanderJitter   = 4f; //Valor pequeño, probar tambien con 10
		
		//wanderTarget es una posicion del circulo, modificamos aleatoriamente la X
		//y la Z, dejando 0 en la Y para que no afecte
        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();		//Normalizamos para que tenga una long de 1
        wanderTarget *= wanderRadius;	//Ahora tendra una long de acuerdo con el radio

        //Ahora el agente esta en el centro del circulo pero el circulo debe de
		//estar a una WanderDistance. Creamos esa posicion enfrente del personaje,
		//es decir, dir. Z forward de agente
        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        //InverseTransformVector: Transformamos coordenadas del mundo a coord locales
        //del NPC, resultando en una posicíon delante del NPC que debemos seguir.
        //Esto varia aleatoriamente frame a frame logrando el comportamiento de wander
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

       Seek(targetWorld);
    }

    void Hide()
    {
        //Elegimos encontrar el "hidingSpot" mas cercano para escondernos
        float dist = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;

        for (int i = 0; i < World.Instance.GetHidingSpots().Length; i++)
        {
            //hideDir es el vector del policia al arbol
            Vector3 hideDir = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            //Para la posicion donde esconderse, vamos un poco mas alla (dejamos un espacio prudente)
            //de la posicion del arbol. Normalizamos la distancia a la unidad y multiplicamos las veces que queremos ir mas alla.
            Vector3 hidePos = World.Instance.GetHidingSpots()[i].transform.position + hideDir.normalized * 10;

            if (Vector3.Distance(this.transform.position, hidePos) < dist)
            {
                chosenSpot = hidePos;
                dist = Vector3.Distance(this.transform.position, hidePos);
            }
        }

        Seek(chosenSpot); //Podriamos hacer simplemente agent.SetDestination()

    }


    //Cada obstaculo hemos asegurado que tenga un collider. Trazamos un rayo (cast a ray) y calculamos
    //donde intersecta el rayo al collider por la parte de atras, esa sera donde nuestro NPC debe ir. Ademas
    //esta sera la distancia mas cercana al obstaculo donde nuestro agente puede estar (sin estar dentro de el).
    void CleverHide()
    {
        //Elegimos encontrar el "hidingSpot" mas cercano para escondernos
        float dist = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDir = Vector3.zero;
        GameObject chosenGO = World.Instance.GetHidingSpots()[0];


        for (int i = 0; i < World.Instance.GetHidingSpots().Length; i++)
        {
            //hideDir es el vector del policia al arbol
            Vector3 hideDir = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            //Para la posicion donde esconderse, vamos un poco mas alla (dejamos un espacio prudente)
            //de la posicion del arbol. Normalizamos la distancia a la unidad y multiplicamos las veces que queremos ir mas alla.
            Vector3 hidePos = World.Instance.GetHidingSpots()[i].transform.position + hideDir.normalized * 10;


            if (Vector3.Distance(this.transform.position, hidePos) < dist)
            {
                chosenSpot = hidePos;
                chosenDir = hideDir;
                chosenGO = World.Instance.GetHidingSpots()[i];
                dist = Vector3.Distance(this.transform.position, hidePos);
            }
        }

        //Hacemos el ray-cast para saber el punto detras del obstaculo (detras del
        //collider) y poder llevar a nuestro agente alli
        Collider hideCol = chosenGO.GetComponent<Collider>();
        Ray backRay = new Ray(chosenSpot, -chosenDir.normalized);
        RaycastHit info;
        float distance = 100.0f; //Asegurarse de que este valor es mayor que
                                 //scalingFactor, si no, la proyeccion del rayo
                                 //nunca lo alcanzara. Así obtenemos el hit point
                                 //de la parte de atras y se guarda en info
        hideCol.Raycast(backRay, out info, distance);


        Seek(info.point + chosenDir.normalized * 2); //Damos un poco mas de margen
                     //sumando chosenDir (nos alejamos un poquito mas)
                     //Recordar que no tenemos que estar fuera del collider, no es
                     //el collider el que nos para, sino que lo utilizamos para
                     //calcular el limite y luego nos acercamos un poco mas

    }


    bool CanSeeTarget()
    {
        RaycastHit raycastInfo;
        Vector3 rayToTarget = target.transform.position - this.transform.position;
        float lookAngle = Vector3.Angle(this.transform.forward, rayToTarget);
        if (lookAngle < 30 && Physics.Raycast(this.transform.position, rayToTarget, out raycastInfo))
        {
            if (raycastInfo.transform.gameObject.tag == "cop")
                return true;
        }
        return false;
    }

    //En el siguiente comportamiento el robber se acerca al cop mientras este no le mira pero, cuando esto sucede, el robber se esconde.
    //Tenemos que decidir cuando el cop ve al rubber, digamos que esto sucede si esta en su angulo de vision (p.e. 60º)
    bool TargetCanSeeMe()
    {
        Vector3 toAgent = this.transform.position - target.transform.position;
        float lookAngle = Vector3.Angle(target.transform.forward, toAgent);
        if (lookAngle < 30)
            return true;
        return false;
    }


    //Tambien vamos a anyadir que durante unos segundos conserve una accion para evitar comportamiento de voy y vengo
    //segun el cop va girandose o mirandolo
    bool coolDown = false;
    void BehaviourCoolDown()
    {
        coolDown = false;
    }

    bool TargetInRange()
    {
        float dist = Vector3.Distance(this.transform.position, target.transform.position);
        if (dist < 3)
            return true;
        return false;
    }


    // Update is called once per frame
    void Update()
    {
        //Seek(target.transform.position);
        //Flee(target.transform.position);
        //Pursue();
        //Evade();
        //Wander();
        //Hide();

       if (!coolDown)
        {
            if (!TargetInRange())
            {
                Wander();
                print("wander");
            }
            else if (CanSeeTarget() && TargetCanSeeMe())
            {
                CleverHide();
                coolDown = true;
                Invoke("BehaviourCoolDown", 5); //Cool down 5 seconds
                print("cleverHide");
            }
            else
                Pursue();

        }
    }

}