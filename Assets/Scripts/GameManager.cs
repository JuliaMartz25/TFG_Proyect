using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> bateries = new List<GameObject>();
   
    // Start is called before the first frame update
    void Start()
    {
        BaterySearch();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BaterySearch()
    {
        //Lista de baterias  
        foreach (GameObject baterias in GameObject.FindGameObjectsWithTag("Batery"))
        {
            bateries.Add(baterias);

        }

      
    }
}
