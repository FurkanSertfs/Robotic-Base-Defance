using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{


    public GameObject resourcesPrefav;
    public Transform[] SpawnPoints;

    float timer;
    public void Update()
    {
    }
    public void Dig()
    {
        int randomIndex = Random.Range(0, SpawnPoints.Length);
        
        Instantiate(resourcesPrefav, SpawnPoints[randomIndex].position, Quaternion.identity);

             
    }

}


