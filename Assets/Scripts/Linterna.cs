using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRInput;
using UnityEngine.UI;

public class Linterna : MonoBehaviour
{
    public Light luz;
    public GameObject linterna, socket;
    RaycastHit hit;
    int layerMask = 1<<6;
    public Enemies enemies;
    public  bool detectado;
    public GameManager manager;
    private void Start()
    {
        luz.enabled = false;
    }
    private void Update()
    {
        
    }
    public void EncenderLinternaR()
    {
       

        if (OVRInput.GetDown(OVRInput.RawButton.A) && luz.enabled == false)
        {
          
            luz.enabled = true;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
                enemies.EnemyHide();
                detectado = true;
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 1000, Color.red);
                Debug.Log("Did not Hit");
                detectado = false;
            }

        }
        else if (OVRInput.GetDown(OVRInput.RawButton.A)  && luz.enabled == true)
        {

            luz.enabled = false;
        }

    }
    public void EncenderLinternaL()
    {
        
        if (OVRInput.GetDown(OVRInput.RawButton.X) && luz.enabled == false)
        {
            luz.enabled = true;

        }
        else if (OVRInput.GetDown(OVRInput.RawButton.X) && luz.enabled == true)
        {

            luz.enabled = false;
        }

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "HandR")
        {
           
            EncenderLinternaR();
    
        }
        else
        {
            //Lanzamiento Bola
            if (OVRInput.GetDown(OVRInput.RawButton.B))
            {

                manager.ThrowingBall();

            }
        }

        if (collision.gameObject.tag == "HandL")
        {
           
            EncenderLinternaL();
 
        }
        else
        {
            //Lanzamiento Bola
            if (OVRInput.GetDown(OVRInput.RawButton.B))
            {

                manager.ThrowingBall();

            }
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "HandR" || collision.gameObject.tag == "HandL")
        {
           
            linterna.transform.position = socket.transform.position;
            linterna.transform.rotation = Quaternion.Euler(0, 0, 180);

            luz.enabled = false;
        }
    }
   
}
