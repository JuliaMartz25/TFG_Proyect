using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody rigibody;
    float timer;
  
    void Update()
    {
        if (this.gameObject == null)
        {
            timer = 0;
        }

        if (this.gameObject != null)
        {
            timer += Time.deltaTime;
            if (timer >= 2f)
            {
               
                rigibody.useGravity = true;
                 timer = 0;
            }
            else
            {
                rigibody.useGravity = false;
            }

        }

    }

    
}
