using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolSize : MonoBehaviour
{
    public int enemyPoolSize = 13; //Tama�o de la array de objetos a reciclar
                                   //aconsejable los posibles sin reusar uno que no est� desactivado
    public GameObject enemy1; //Referencia temporal para gesti�n del array con objetos prefabs
    private GameObject[] enemies; //Array de objetos a reciclar
    public int spwanNumber = -1; //N�mero con la posici�n del array que toca activar y gestionar
    public Transform spawnpos;

    void Start()
    {
       

        //Creamos la array con un tama�o igual al de la variable int primera
        enemies = new GameObject[enemyPoolSize];
        //De forma secuencia gracias a un bucle for creamos todas las balas, recordar
        //que estas comienzan desactivadas con lo cual no se ejecutar� su script y
        //saldr�n todas disparadas a la vez. Tambi�n fijaos en la posici�n de creaci�n:
        // el valor -10 de la �x� hace que est�n fuera de la escena por seguridad.
        for (int i = 0; i <enemyPoolSize; i++)
        {
            enemies[i] = Instantiate(enemy1, new Vector2(0, -10), Quaternion.identity);
            enemy1.SetActive(false);
        }
    }
}
