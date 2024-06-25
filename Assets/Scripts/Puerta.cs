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
            Destroy(collision.gameObject);

            _manager.enemiesIngame.Remove(this.gameObject);
        }
    }
   
}
