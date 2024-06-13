using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
     NavMeshAgent agent;
     public List <GameObject> bateries = new List<GameObject>();
     public int currentBatery;
  

    private void Start()
    {
        //Lista de baterias  
        foreach (GameObject baterias in GameObject.FindGameObjectsWithTag("Batery"))
        {
            bateries.Add(baterias);
          
        }
       
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        EnemyMovement();
       
    }
    void EnemyMovement()
    {
        float value = Mathf.Infinity; // Resetea el valor 

        // Recorro la array de baterias en busca de la mas cercana
        for (int i = 0; i < bateries.Count; i++)
        {
            // Si la bateria es la mas cercana voy a ella
            float baterydist = Vector3.Distance(bateries[i].transform.position, agent.transform.position);

            //Si el valor de la distancia es menor, entonces la bateria es i y actualizamos el valor
            if (baterydist < value)
            {
                currentBatery = i;
                value = baterydist;
            }
        }

        // Si siguen quedando baterias entonces envio al enemigo a la bateria mas cercana
        if (bateries.Count > 0)
        {
            agent.SetDestination(bateries[currentBatery].transform.position);
        }

      
    }

   
}

