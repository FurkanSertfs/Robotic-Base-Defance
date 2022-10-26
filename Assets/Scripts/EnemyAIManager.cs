using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class EnemyAIManager : AIManager
{

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        agent = GetComponent<NavMeshAgent>();
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


        if (!isActiceFire)
        {
            StartCoroutine(Fire(fireRate));

        }


        if (_targetEnemy != null)
        {
            var lookPos = _targetEnemy.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
        }

        else
        {
            FindEnemy();
        }


       


    }

    void FindEnemy()
    {
        if (enemiesInRange.Count > 0)
        {
            if (enemiesInRange[0] != null)
            {
                _targetEnemy = enemiesInRange[0].transform;
            }
            else
            {
                enemiesInRange.RemoveAt(0);
            }

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DefanceArea>())
        {
            other.GetComponent<DefanceArea>().enemyAIManagers.Add(this);
        }
    }

    private void OnDisable()
    {
        if (BaseDefanceManager.baseDefanceManager!=null)
        {
            for (int i = 0; i < BaseDefanceManager.baseDefanceManager.defanceAreas.Count; i++)
            {
                for (int j = 0; j < BaseDefanceManager.baseDefanceManager.defanceAreas[i].defanceArea.enemyAIManagers.Count; j++)
                {
                    BaseDefanceManager.baseDefanceManager.defanceAreas[i].defanceArea.enemyAIManagers.Remove(this);
                }
            }
        }

        
    }










}
