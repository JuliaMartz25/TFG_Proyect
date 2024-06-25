using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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
   

    //OLEADAS
    /*public GameObject[] spawnPoints;
    public GameObject[] enemies;
    public int waveCount;
    public int wave;
    public int enemyType;
    public bool spawning;
    private int enemiesSpawned;
    public int enemiesdefeat;*/

    void Start()
    {
        EnemiesIngame();
        BaterySearch();
        DoorSearch();

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

        /*waveCount = 2; //Enemigos que aumentan cada oleada
        wave = 1;
        spawning = false;
        enemiesSpawned = 0;*/
    }
    private void Update()
    {
        if (enemiesIngame.Count<=0)
        {
            Win();
        }
    }
    /*private void Update()
    {
        if(spawning == false && enemiesSpawned == enemiesdefeat) 
        {
            StartCoroutine(SpawnWave(waveCount));
        }
    }
    IEnumerator SpawnWave(int waveC)
    {
        spawning = true;
        yield return new WaitForSeconds(4); 

        for(int i = 0;i < waveC;i++)
        {
            SpawnEnemy(wave);
            yield return new WaitForSeconds(2);
        }
        wave += 1;
        waveCount += 2;
        spawning = false;

        yield break;
    }
    void SpawnEnemy(int wave)
    {
        int spawnPos = Random.Range(0,4);
        if (wave ==1)
        {
            enemyType = 1;
        }
        else if((wave <4))
        {
            enemyType = Random.Range(0,2);
        }
        else
        {
            enemyType = Random.Range(0, 3);
        }


        Instantiate(enemies[enemyType], spawnPoints[spawnPos].transform.position, spawnPoints[spawnPos].transform.rotation);
        enemiesSpawned++;


    }*/
    //OLEADAS FIN
    public void EnemiesIngame()
    {
        //Lista de baterias  
        foreach (GameObject baterias in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemiesIngame.Add(baterias);

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

   public void Jugar()
    {
        SceneManager.LoadScene(1);
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

