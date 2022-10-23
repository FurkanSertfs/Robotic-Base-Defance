using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIManager : MonoBehaviour
{
    public Transform _targetEnemy;

    public NavMeshAgent agent;

    public Animator animator;

    public float fireRate;

    public float range;

    [SerializeField]
    Transform firePoint;
   
    [SerializeField]
    GameObject bullet;

    public List<GameObject> enemiesInRange;

    public float health=100;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
      
        agent = GetComponent<NavMeshAgent>();
    }

    public void GotoTarget(Transform point)
    {

        agent.SetDestination(point.position);

        agent.isStopped = false;

        animator.SetBool("isRun", true);

    }
    public bool CheckArrive(float range)
    {
        if (agent.hasPath && agent.remainingDistance < range)
        {
            animator.SetBool("isRun", false);

            agent.isStopped = true;

            return true;
        }

        return false;
    }

    public IEnumerator Fire()
    {
     

        yield return new WaitForSeconds(fireRate);

        if (enemiesInRange.Count > 0)
        {
            GameObject newBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
          
            StartCoroutine(Fire());

            Vector3 shootDir = (enemiesInRange[0].transform.position - firePoint.position).normalized;

            newBullet.GetComponent<Bullet>().Setup(shootDir, enemiesInRange[0].transform);

        }
    }




    public void CheckArrive()
    {
        if (agent.hasPath && agent.remainingDistance <0.1f)
        {
            animator.SetBool("isRun", false);

            agent.isStopped = true;

        }
    }

  
}

        



  



