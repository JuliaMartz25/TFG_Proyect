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
    public RaycastHit hit;
    int layerMask = 1 << 6; // Asegúrate de que tus NPCs estén en la capa 6
    public GameManager gameManager;
    public bool encendida;

    private void Start()
    {
        encendida = true;
    }

    private void Update()
    {
       

        if (encendida)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
                Debug.Log("Did Hit: " + hit.collider.gameObject.name);
                hit.collider.gameObject.SendMessage("OnRaycastHit", SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 1000, Color.red);
                Debug.Log("Did not Hit");
            }
        }
    }

    public void EncenderLinternaR()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            luz.enabled = !luz.enabled;
            encendida = luz.enabled;
        }
    }

    public void EncenderLinternaL()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.X))
        {
            luz.enabled = !luz.enabled;
            encendida = luz.enabled;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "HandR")
        {
            EncenderLinternaR();
        }
        else if (collision.gameObject.tag == "HandL")
        {
            EncenderLinternaL();
        }
       
            if (collision.gameObject.tag != "HandR")
            {
                //Lanzamiento Bola
                if (OVRInput.GetDown(OVRInput.RawButton.B))
                {

                    gameManager.ThrowingBall();

                }
            }

          

            //Si no tengo la linterna en la mano
            if (collision.gameObject.tag != "HandL")
            {
                //Lanzamiento Bola
                if (OVRInput.GetDown(OVRInput.RawButton.B))
                {

                    gameManager.ThrowingBall();

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
