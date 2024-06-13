using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;



public class Bateria : MonoBehaviour
{
   [SerializeField] private  Image barra;
    private float vidaMaxima =100f;
    [SerializeField] private float vidaActual =100;
    private float danyo = 5f;
    private float timer;
    public Enemies bateria;
  
  


    private Renderer renderObj;
    public Color defaultcolor;
    public Color newcolor;
    private void Start()
    {
        renderObj = GetComponent<Renderer>();
        renderObj.material.color = defaultcolor;
    

    }
    // Update is called once per frame
    void Update()
    {
        Vida();

    }
    private void Vida()
    {
        barra.fillAmount = vidaActual / vidaMaxima;
        
    }
   
    private void CambiarColor(Color color, float timer)
    {
        if (timer >=1)
        {
            renderObj.material.color = color;
        }
       
    }

    private void OnCollisionStay(Collision collision)
    {
    
        if (collision.gameObject.tag == "Enemy")
        {
            timer +=Time.deltaTime;
         
          
            if (timer >= 3)
            {
                vidaActual -= danyo;
                CambiarColor(newcolor,timer);
                timer = 0;
            }
            else
            {
                CambiarColor(defaultcolor, timer);
            }

            //Si lavida es menor o igual a cero eliminar la bateria

            if (vidaActual <= 0)
            {
                vidaActual = 0;

                bateria.bateries.Remove(bateria.bateries[bateria.currentBatery]);
                Destroy(this.gameObject);
             
            }


        }

       
    }
}
