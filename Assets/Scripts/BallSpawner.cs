using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject proyectil;
    
    // Start is called before the first frame update
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            Instantiate(proyectil, this.transform);
            print("Invoko ball");
        }
    }

}
