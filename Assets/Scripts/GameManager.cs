using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> bateries = new List<GameObject>();
    public List<GameObject> doors = new List<GameObject>();
    public List<GameObject>enemiesIngame = new List<GameObject>();

   

    public GameObject canvasGameOver, canvaswWin;

    //BALL POOL SIZE
    public int ballPoolSize = 13; //Tamaño de la array de objetos a reciclar
                                    //aconsejable los posibles sin reusar uno que no esté desactivado
    public GameObject ball; //Referencia temporal para gestión del array con objetos prefabs
    private GameObject[] balls; //Array de objetos a reciclar
    public int throwNumber = -1; //Número con la posición del array que toca activar y gestionar
    public Transform ballspawnpos;

    public Text textTimer, oleada;
    public float Timer; 
   
    public List<Transform> enemiespos = new List<Transform>();
    public GameObject enemy1, enemy2, enemy3;
    public static int currentWave = 0;
    
    void Start()
    {
        EnemiesIngame();
        BaterySearch();
        DoorSearch();
        EnemiesSpaenPos();

        //Creamos la array con un tamaño igual al de la variable int primera
        balls = new GameObject[ballPoolSize];
        //De forma secuencia gracias a un bucle for creamos todas las balas, recordar
        //que estas comienzan desactivadas con lo cual no se ejecutará su script y
        //saldrán todas disparadas a la vez. También fijaos en la posición de creación:
        // el valor -10 de la “x” hace que estén fuera de la escena por seguridad.
        for (int i = 0; i < ballPoolSize; i++)
        {
            balls[i] = Instantiate(ball, new Vector2(0, -10), Quaternion.identity);
            ball.SetActive(false);
        }

        
    
    }
    private void Update()
    {
        Timer += Time.deltaTime;
       
        int minutes = Mathf.FloorToInt(Timer /60);
        int seconds = Mathf.FloorToInt(Timer %60);
        textTimer.text = string.Format("{0:00}:{1:00}",minutes,seconds);
        oleada.text = "OLEADA "+currentWave+" SUPERADA";
        if(minutes >= 3 && seconds >= 0)
        {
            minutes = 3;
            seconds = 0;
            GameOver();
            Time.timeScale = 0;
        }

        if (enemiesIngame.Count==0 && minutes <= 3 )
        {
            Win();
           
        }

      
        
    }

    public void EnemiesSpaenPos()
    {
      
        foreach (GameObject posiciones in GameObject.FindGameObjectsWithTag("Posiciones"))
        {
            enemiespos.Add(posiciones.transform);

        }
    }
    public void EnemiesIngame()
    {
        //Lista de enemigos en escena 
        foreach (GameObject enemies in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemiesIngame.Add(enemies);

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
        //Lista de puertas
        foreach (GameObject puertas in GameObject.FindGameObjectsWithTag("Puerta"))
        {
           doors.Add(puertas);

        }
    }
    public void ThrowingBall()
    {
        //Cada vez que disparemos el “puntero” del array aumenta en uno para que
        //en el siguiente disparo señale a la siguiente bala del array.
        throwNumber++;
        //En el caso de que el puntero supere el número de posiciones del array
        //vuelve a 0 para seguir con el proceso.
        if (throwNumber > 12)
        {
            throwNumber = 0;
        }
        //Ponemos la bala, desactivada aún, en la posición del objeto “GunObject”
        balls[throwNumber].transform.position = ballspawnpos.transform.position ;
        //¡Activamos la bala!
        balls[throwNumber].SetActive(true);
    }

    public void OleadasEnemigos()
    {
        if (currentWave == 0)
        {
            Instantiate(enemy1, enemiespos[Random.Range(0, 4)]);
        }

        if (currentWave == 1)
        {
            Instantiate(enemy1, enemiespos[Random.Range(0, 4)]);
            StartCoroutine("Aparicion2");
            Instantiate(enemy2, enemiespos[Random.Range(0, 4)]);
        }

        if (currentWave == 2)
        {
            Instantiate(enemy1, enemiespos[Random.Range(0, 4)]);
            StartCoroutine("Aparicion2");
            Instantiate(enemy2, enemiespos[Random.Range(0, 4)]);
            StartCoroutine("Aparicion");
            Instantiate(enemy3, enemiespos[Random.Range(0, 4)]);
        }
    }

    IEnumerator Aparicion2()
    {
        yield return new WaitForSeconds(2f);
    }

    IEnumerator Aparicion()
    {
        yield return new WaitForSeconds(4f);
    }


    public void Jugar()
    {
        SceneManager.LoadScene(1);
    }
    public void Seguir()
    {
        SceneManager.LoadScene(1);
        currentWave++;
    }
    public void SalirMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Salir()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        canvasGameOver.SetActive(true);
    }
    public void Win()
    {
        canvaswWin.SetActive(true);
    }





}



/*//Hide Objects
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
}*/

