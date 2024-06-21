using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    public NavMeshAgent agent;
    public int currentBatery, currentPuerta;
    public GameObject target;
    public GameManager manager;
    public Linterna linterna;
 
  

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
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
            print("You lose");

        }

        
      
    }

    public void EnemyHide()
    {
        
        float value = Mathf.Infinity; // Resetea el valor 

        // Recorro la array de puertas en busca de la mas cercana
        for (int i = 0; i < manager.puertas.Count; i++)
        {
            // Si la bateria es la mas cercana voy a ella
            float puertadist = Vector3.Distance(manager.puertas[i].transform.position, agent.transform.position);

            //Si el valor de la distancia es menor, entonces la bateria es i y actualizamos el valor
            if (puertadist < value)
            {
                currentPuerta = i;
                value = puertadist;
            }
        }

        // Si siguen quedando baterias entonces envio al enemigo a la bateria mas cercana
        if (manager.puertas.Count > 0)
        {
            agent.SetDestination(manager.bateries[currentPuerta].transform.position);
           
        }
    }


}

