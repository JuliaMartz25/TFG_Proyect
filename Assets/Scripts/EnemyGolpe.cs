using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGolpe : Enemies
{
   
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Linterna");
        agent = gameObject.GetComponent<NavMeshAgent>();
        manager = FindFirstObjectByType<GameManager>();
       


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
            manager.enemiesIngame.Remove(this.gameObject);
            gameObject.SetActive(false);
          
        }
    }


}
