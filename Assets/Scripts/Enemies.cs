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
    public GameObject player;

    private bool colision;
    private bool isHitByRay; // Nuevo flag para detectar el golpe del rayo

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Linterna");
        agent = gameObject.GetComponent<NavMeshAgent>();
        manager = FindFirstObjectByType<GameManager>();
        // linterna1 y linterna2 ya no son necesarios aquí
    }

    private void Update()
    {
        if (isHitByRay)
        {
            EnemyHide(this.gameObject);
        }
        else
        {
            EnemyMovement();
        }

        // Resetear el estado de hit para la próxima verificación
        isHitByRay = false;
    }

    public void OnRaycastHit()
    {
        // Este método será llamado por la linterna cuando golpee a este enemigo
        isHitByRay = true;
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
            manager.canvasGameOver.SetActive(true);
        }
    }

    public void EnemyHide(GameObject _agent)
    {
        float value = Mathf.Infinity; // Resetea el valor 

        // Recorro la array de puertas en busca de la mas cercana
        for (int i = 0; i < manager.doors.Count; i++)
        {
            // Si la bateria es la mas cercana voy a ella
            float puertadist = Vector3.Distance(manager.doors[i].transform.position, _agent.transform.position);

            //Si el valor de la distancia es menor, entonces la bateria es i y actualizamos el valor
            if (puertadist < value)
            {
                currentPuerta = i;
                value = puertadist;
            }
        }

        // Si siguen quedando puertas entonces envio al enemigo a la puerta más cercana
        if (manager.doors.Count > 0)
        {
            agent.SetDestination(manager.doors[currentPuerta].transform.position);
        }
    }
}

