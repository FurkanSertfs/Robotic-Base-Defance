using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIManager : MonoBehaviour
{
    public Transform target;

    [SerializeField]
    NavMeshAgent agent;

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

}
