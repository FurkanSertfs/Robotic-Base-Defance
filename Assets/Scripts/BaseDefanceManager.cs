using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseDefanceManager : MonoBehaviour
{
    [SerializeField]
    SoldierPosition[] soldierPositions;

    public static BaseDefanceManager baseDefanceManager;

    private void Awake()
    {
        baseDefanceManager = this;
    }

    public void AddSoldier(AIManager aIManager)
    {
        for (int i = 0; i < soldierPositions.Length; i++)
        {
            if (soldierPositions[i].aIManager == null)
            {
                
                aIManager.soldierPosition = soldierPositions[i].defancePosition;
               
                aIManager.agent.SetDestination(aIManager.soldierPosition.position);

                aIManager.animator.SetBool("isRun", true);

                soldierPositions[i].aIManager = aIManager;

                break;
            }
        }
    }
  

    
}


