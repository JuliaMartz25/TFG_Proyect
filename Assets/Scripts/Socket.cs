using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    [SerializeField] GameObject FlashLight;
    
    // Update is called once per frame
    void Update()
    {
        float distanceFlashLight = Vector3.Distance(FlashLight.transform.position,this.transform.position);
     
        if( distanceFlashLight<0.5)
        {
           FlashLight.transform.SetParent(this.gameObject.transform);
           FlashLight.transform.localPosition = this.gameObject.transform.position;
        }
        else
        {
            FlashLight.transform.SetParent(null);
  
        }



    }
}
