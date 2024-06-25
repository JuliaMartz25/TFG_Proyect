using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    public Linterna linterna1, linterna2;
    public GameManager _manager;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
             _manager.enemiesIngame.Remove(collision.gameObject);

            collision.gameObject.SetActive(false);

           
        }
    }
   
}
