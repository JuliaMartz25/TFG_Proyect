using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : MonoBehaviour
{
    public Bateria bateria;
    public GameManager gameManager;
    public Enemies enemies;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "HandR" || collision.gameObject.tag == "HandL")
        {


            bateria.vidaActual += 10;

            bateria.renderObj.material.color = bateria.greencolor;

            if (bateria.vidaActual>= 100)
            {
                bateria.vidaActual = 100;

                gameManager.bateries.Add(gameManager.bateries[enemies.currentBatery]);
            }

           
        }
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "HandR" || collision.gameObject.tag == "HandL")
        {
            bateria.renderObj.material.color = bateria.defaultcolor;
        }
            
    }
}
