using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


public class Enemies : MonoBehaviour
{
    public NavMeshAgent agent;
    public int currentBatery, currentPuerta;
    public GameManager manager;
    public Linterna linterna;
    public GameObject canvas;
 
  

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        manager = FindFirstObjectByType<GameManager>();
        linterna = FindFirstObjectByType<Linterna>();
        // EEEROR CANVAS GAME OBJECT HAY QUE PONERLO NE JERARQUIA canvas = FindFirstObjectByType<GameObject>();




    }

    private void Update()
    {
        if (linterna.detectado ==true)
        {
          
            EnemyHide();
        }
        else
        {
            EnemyMovement();
        }
      
       
  
    }

   
   public void EnemyMovement()
    {
        float value = Mathf.Infinity; // Resetea el valor 

        // Recorro la array de baterias en busca de la mas cercana
        for (int i = 0; i < manager.bateries.Count; i++)
        {
            // Si la bateria es la mas cercana voy a ella
            float baterydist = Vector3.Distance(manager.bateries[i].transform.position, agent.transform.position);

            //Si el valor de la distancia es menor, entonces la bateria es i y actualizamos el valor
            if (baterydist < value)
            {
                currentBatery = i;
                value = baterydist;
            }
        }

        // Si siguen quedando baterias entonces envio al enemigo a la bateria mas cercana
        if (manager.bateries.Count > 0)
        {
            agent.SetDestination(manager.bateries[currentBatery].transform.position);
        }
        else
        {
            agent.speed = 0; 
           canvas.SetActive(true);

        }

        
      
    }

    public void EnemyHide()
    {
        
        float value = Mathf.Infinity; // Resetea el valor 

        // Recorro la array de puertas en busca de la mas cercana
        for (int i = 0; i < manager.doors.Count; i++)
        {
            // Si la bateria es la mas cercana voy a ella
            float puertadist = Vector3.Distance(manager.doors[i].transform.position, agent.transform.position);

            //Si el valor de la distancia es menor, entonces la bateria es i y actualizamos el valor
            if (puertadist < value)
            {
                currentPuerta = i;
                value = puertadist;
            }
        }

        // Si siguen quedando baterias entonces envio al enemigo a la bateria mas cercana
        if (manager.doors.Count > 0)
        {
            agent.SetDestination(manager.doors[currentPuerta].transform.position);
           
        }
    }


}

