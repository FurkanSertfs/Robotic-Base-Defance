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

    public GameObject gun;
    private void Start()
    {
        startHealth = health;

        _bodyPartManager = GetComponent<BodyPartManager>();

        animator = GetComponentInChildren<Animator>();

        agent = GetComponent<NavMeshAgent>();

        BaseDefanceManager.baseDefanceManager.AddSoldier(this);

        defanceAreas = BaseDefanceManager.baseDefanceManager.defanceAreas;
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

        CheckArrive();

        FindEnemy();

        if (!isActiceFire)
        {
            if (_bodyPartManager.noLeg && !animator.GetBool("isRun"))
            {
                StartCoroutine(Fire(fireRate));
            }

            else if (!_bodyPartManager.noLeg)
            {
                StartCoroutine(Fire(fireRate));
            }

          

        }

        if (_targetEnemy != null)
        {
            var lookPos = _targetEnemy.position - transform.position;
            var gunLookPos = _targetEnemy.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            var gunRotation = Quaternion.LookRotation(gunLookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
         //   gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation, gunRotation, Time.deltaTime * 2);
        }


        if (positionState==PositionState.Attack)
        {

            if (_targetEnemy != null)
            {
              
                GotoTarget(_targetEnemy);

                CheckArrive(range);

            }

        }

     




        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeHit(transform, 8);
        }




    }
    public void ChangeGunPosition(Transform point)
    {
        gun.transform.parent = point;
        gun.transform.position = point.position;
        gun.transform.rotation = point.rotation;
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
        

        for (int i = 0; i < shouldDestroyPart - _destroyedPart; i++)
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
        if (enemiesInRange.Count==0)
        {
            for (int i = 0; i < defanceAreas.Count; i++)
            {


                for (int j = 0; j < defanceAreas[i].defanceArea.enemyAIManagers.Count; j++)
                {
                    if (defanceAreas[i].defanceArea.enemyAIManagers[j] != null)
                    {
                        _targetEnemy = defanceAreas[i].defanceArea.enemyAIManagers[j].transform;

                        return true;
                    }

                    else
                    {
                        defanceAreas[i].defanceArea.enemyAIManagers.RemoveAt(j);
                    }



                }


            }
        }


       

        return false;
    }

}
[System.Serializable]
public class DefanceAreas
{
    public DefanceArea defanceArea;
}
