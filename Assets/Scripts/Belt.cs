using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    GameObject linterna, proyectil;
    
    public void Update()
    {
        float distanceLint = Vector3.Distance(linterna.transform.position, this.transform.position);
        float distanceProy= Vector3.Distance(proyectil.transform.position, this.transform.position);
        if (distanceLint<1f)
        {
           linterna.transform.SetParent(this.transform);
        }
        if (distanceProy<1f)
        {
            proyectil.transform.SetParent(this.transform);
        }


    }
        
       
}
