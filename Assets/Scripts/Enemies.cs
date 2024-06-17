using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    public NavMeshAgent agent;
    public int currentBatery;

    public GameManager manager;
  

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        EnemyMovement();
        


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

   
}

