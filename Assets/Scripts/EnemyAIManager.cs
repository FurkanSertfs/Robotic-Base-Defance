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

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DefanceArea>())
        {
            other.GetComponent<DefanceArea>().enemyAIManagers.Add(this);
        }
    }

  








}
