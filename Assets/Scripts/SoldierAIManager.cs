using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierAIManager : AIManager
{
 
    public PositionState positionState;

    public List<DefanceAreas> defanceAreas;

    int _destroyedPart;

    int _shouldDestroyPart;

    float startHealth;

    BodyPartManager _bodyPartManager;
    private void Start()
    {
        startHealth = health;

        _bodyPartManager = GetComponent<BodyPartManager>();

        animator = GetComponentInChildren<Animator>();

        agent = GetComponent<NavMeshAgent>();

        BaseDefanceManager.baseDefanceManager.AddSoldier(this);
    }



    private void Update()
    {


        if (agent.hasPath)
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }



        if (positionState==PositionState.Attack)
        {
            if (_targetEnemy != null)
            {
                if (FindEnemy())
                {
                    GotoTarget(_targetEnemy);
                }
            }

            else
            {
                if (CheckArrive(range))
                {
                    Fire();
                }
            }

        }

        else if (positionState == PositionState.Defance)
        {
            Fire();

        }

        else if (positionState == PositionState.State)
        {
            CheckArrive();
           
           

        }




        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeHit(transform, 8);
        }




    }

    public void TakeHit(Transform point,int damage)
    {
        // Instantiate(bloodParticicaular , point)

        health -= damage;

        if (health > 0)
        {
            SelectRandomPart((int)((startHealth - health) / (startHealth / 4)));
        }

       


       

    }

    void SelectRandomPart(int shouldDestroyPart)
    {
        Debug.Log(shouldDestroyPart);

        for (int i = 0; i < shouldDestroyPart-_destroyedPart; i++)
        {
            int randomPart = Random.Range(2, 6);

            if (_bodyPartManager.isHandDestroyed)
            {
                while (_bodyPartManager.bodyTypeHealths[randomPart].isDestroyed || randomPart ==2 || randomPart == 3)
                {
                    randomPart = Random.Range(2, 6);

                }

            }

            else
            {
                while (_bodyPartManager.bodyTypeHealths[randomPart].isDestroyed)
                {
                    randomPart = Random.Range(2, 6);

                }
            }

           

            _bodyPartManager.DestrotBodyPart(randomPart);

        }

        _destroyedPart = shouldDestroyPart;

    }



    public bool FindEnemy()
    {

        for (int i = 0; i < defanceAreas.Count; i++)
        {
            for (int j = 0; j < defanceAreas[i].defanceArea.enemyAIManagers.Count; j++)
            {
                _targetEnemy = defanceAreas[i].defanceArea.enemyAIManagers[j].transform;

                return true;
            }

            
        }

        return false;
    }

}

public class DefanceAreas
{
    public DefanceArea defanceArea;
}
