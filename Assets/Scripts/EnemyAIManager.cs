using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class EnemyAIManager : AIManager
{
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DefanceArea>())
        {
            other.GetComponent<DefanceArea>().enemyAIManagers.Add(this);
        }
    }









}
