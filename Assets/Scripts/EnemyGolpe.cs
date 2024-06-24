using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGolpe : Enemies
{
   
    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        manager = FindFirstObjectByType<GameManager>();
        linterna = FindFirstObjectByType<Linterna>();
       // ERROR canvas = gameObject.GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
     
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Destroy(this.gameObject);
            manager.enemiesdefeat++;
        }
    }


}