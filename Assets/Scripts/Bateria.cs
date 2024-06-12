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
    Enemies bateria;

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
   

    private void OnCollisionStay(Collision collision)
    {
    
        if (collision.gameObject.tag == "Enemy")
        {
            timer +=Time.deltaTime;
           

            if (timer >= 3)
            {
                vidaActual -= danyo;
                renderObj.material.color = newcolor;
                timer = 0;

               
            }
           
           
                
            
        }

        if (vidaActual <= 0)
        {
            vidaActual = 0;

           
            Destroy(this.gameObject);
        }
    }
}
