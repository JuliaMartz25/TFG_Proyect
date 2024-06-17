using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static OVRInput;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject proyectil;
    [SerializeField] private Controller controller;
    [SerializeField] private Transform pos;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Socket" && OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger)>=0f || OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) >= 0f)
        {
            Instantiate(proyectil, pos);
          
  
        }
    }

}
