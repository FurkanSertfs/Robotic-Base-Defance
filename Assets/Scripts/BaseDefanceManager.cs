using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BaseDefanceManager : MonoBehaviour
{
    public List<DefanceAreas> defanceAreas;

    [SerializeField]
    SoldierPosition[] soldierPositions;

    public static BaseDefanceManager baseDefanceManager;

    [SerializeField]
    Image processBar;

    bool _playerIsHere;

    [HideInInspector]
    public List<AIManager> soldierList = new List<AIManager>();
    
    [SerializeField]

    Transform attackPoint, soldiersParent;

    EnemyManager _enemyManager;

    private void Awake()
    {
        baseDefanceManager = this;

    }

    private void Start()
    {
        EnemyManager.enemyManager = _enemyManager;
    }


    public void AddSoldier(SoldierAIManager aIManager)
    {
        for (int i = 0; i < soldierPositions.Length; i++)
        {
            if (soldierPositions[i].aIManager == null)
            {   
                aIManager.GotoTarget(soldierPositions[i].defancePosition);

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
      

        for (int i = 0; i < soldierPositions.Length; i++)
        {
            if (soldierPositions[i].aIManager!=null)
            {
                soldierPositions[i].aIManager.GotoTarget(attackPoint);

                soldierPositions[i].transform.parent = soldiersParent;

                soldierPositions[i].aIManager.positionState = AIManager.PositionState.Attack;

                soldierPositions[i].aIManager = null;

               

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


