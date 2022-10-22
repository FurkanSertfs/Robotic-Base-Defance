using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BaseDefanceManager : MonoBehaviour
{
    [SerializeField]
    SoldierPosition[] soldierPositions;

    public static BaseDefanceManager baseDefanceManager;

    [SerializeField]
    Image processBar;

    bool _playerIsHere;

    [SerializeField]
    Transform[] _waitingAttackPoints;

    
    public int waitingSoldierCount;

   

    [HideInInspector]
    public List<AIManager> soldierList = new List<AIManager>();
    public List<AIManager> waitingSoldierList = new List<AIManager>();


    [SerializeField]

    Transform attackPoint, soldiersParent;

    private void Awake()
    {
        baseDefanceManager = this;

 
    }

    private void Update()
    {
        if (waitingSoldierCount> 0 & waitingSoldierCount == waitingSoldierList.Count)
        {

            for (int i = 0; i < waitingSoldierList.Count; i++)
            {
                waitingSoldierList[i].GotoTarget(attackPoint,"attackPoint");
              
                soldierList.Add(waitingSoldierList[i]);
            }

            waitingSoldierList.Clear();




        }
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

    public void FillAttackBar(PlayerController playerController,bool playerIsHere)
    {
        _playerIsHere = playerIsHere;

        if(_playerIsHere)
        {
            StartCoroutine(FillTheBar());
        }
        
    }

    void Attack()
    {
        int index = 0;

        for (int i = 0; i < soldierPositions.Length; i++)
        {
            if (soldierPositions[i].aIManager!=null)
            {
                soldierPositions[i].aIManager.GotoTarget(_waitingAttackPoints[index], "waitingPoint");

                soldierPositions[i].transform.parent = soldiersParent;


                soldierPositions[i].aIManager = null;

                index++;
                
                waitingSoldierCount = index;
            }

        }

    }


  
   IEnumerator FillTheBar()
    {
        processBar.fillAmount += 0.05F;
        
        yield return new WaitForSeconds(0.05f);

        if (processBar.fillAmount>= 1)
        {
            processBar.fillAmount = 0;

            Attack();
         
        }

        else if (_playerIsHere)
        {
            StartCoroutine(FillTheBar());
        }

        else if (!_playerIsHere)
        {
            processBar.fillAmount = 0;
        }


   
    }
    



}


