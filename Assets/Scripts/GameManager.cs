using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> bateries = new List<GameObject>();
    public List<GameObject> puertas = new List<GameObject>();

    //BALL POOL SIZE
    public int ballPoolSize = 13; //Tama�o de la array de objetos a reciclar
                                    //aconsejable los posibles sin reusar uno que no est� desactivado
    public GameObject ball; //Referencia temporal para gesti�n del array con objetos prefabs
    private GameObject[] balls; //Array de objetos a reciclar
    public int throwNumber = -1; //N�mero con la posici�n del array que toca activar y gestionar
    public Transform ballspawnpos;


    void Start()
    {
        BaterySearch();
        DoorSearch();

        //Creamos la array con un tama�o igual al de la variable int primera
        balls = new GameObject[ballPoolSize];
        //De forma secuencia gracias a un bucle for creamos todas las balas, recordar
        //que estas comienzan desactivadas con lo cual no se ejecutar� su script y
        //saldr�n todas disparadas a la vez. Tambi�n fijaos en la posici�n de creaci�n:
        // el valor -10 de la �x� hace que est�n fuera de la escena por seguridad.
        for (int i = 0; i < ballPoolSize; i++)
        {
            balls[i] = Instantiate(ball, new Vector2(0, -10), Quaternion.identity);
            ball.SetActive(false);
        }
    }

   
    public void BaterySearch()
    {
        //Lista de baterias  
        foreach (GameObject baterias in GameObject.FindGameObjectsWithTag("Batery"))
        {
            bateries.Add(baterias);

        }

      
    }
    public void DoorSearch()
    {
        //Lista de baterias  
        foreach (GameObject baterias in GameObject.FindGameObjectsWithTag("Puerta"))
        {
            puertas.Add(baterias);

        }


    }
    public void ThrowingBall()
    {
        //Cada vez que disparemos el �puntero� del array aumenta en uno para que
        //en el siguiente disparo se�ale a la siguiente bala del array.
        throwNumber++;
        //En el caso de que el puntero supere el n�mero de posiciones del array
        //vuelve a 0 para seguir con el proceso.
        if (throwNumber > 12)
        {
            throwNumber = 0;
        }
        //Ponemos la bala, desactivada a�n, en la posici�n del objeto �GunObject�
        balls[throwNumber].transform.position = ballspawnpos.transform.position ;
        //�Activamos la bala!
        balls[throwNumber].SetActive(true);
    }

   public void Jugar()
    {
        SceneManager.LoadScene(1);
    }
   public void Salir()
    {
        Application.Quit();
    }
}

//Hide Objects
public sealed class World
{
    private static readonly World instance = new World();
    private static GameObject[] hidingspots;

    static World()
    {
        hidingspots = GameObject.FindGameObjectsWithTag("Hide");
    }

    public static World Instance
    {
        get { return instance; }
    }
    public GameObject[] GetHidingSpots() { return hidingspots; }
}