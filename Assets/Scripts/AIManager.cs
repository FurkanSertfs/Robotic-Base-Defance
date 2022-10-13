using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIManager : MonoBehaviour
{
    public Transform target,soldierPosition;

    public GameObject player;

    public NavMeshAgent agent;

    public List<GameObject> enemies = new List<GameObject>();

    public Animator animator;

    [SerializeField]
    float fireSpeed;

    [SerializeField]
    Transform firePoint;
    [SerializeField]
    GameObject bullet;

   
    public float health=100;


    private bool isActiveFire;

    [SerializeField]
    RuntimeAnimatorController controller;

    public bool isHaveTarget;

    

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void FollowPlayer()
    {
        transform.position = target.position;
      
        transform.parent = target.transform;

    }
   


    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (soldierPosition != null && agent.isActiveAndEnabled )
        {

            if (agent.remainingDistance < 1)
            {
                agent.enabled = false;
                animator.SetBool("isRun", false);
            }


           
        }


        if (enemies.Count > 0)
        {
              animator.SetBool("isFire", true);
            if (enemies[0] != null)
            {
                var lookPos = enemies[0].transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 4);



                //   transform.LookAt(enemies[0].transform);

                if (!isActiveFire)
                {
                    StartCoroutine(FireRoutine(enemies[0]));

                }
            }
            else
            {
                enemies.RemoveAt(0);
            }

           

        }
        else
        {
              animator.SetBool("isFire", false);
        }


        if (player != null)
        {
            if (enemies.Count<=0)
            {
                transform.rotation = player.transform.rotation;
            }

            transform.localPosition = new Vector3(0, 0, 0);

            animator.SetBool("isRun", player.GetComponent<Animator>().GetBool("isRun"));
        }
    }


    private void OnTriggerEnter(Collider other)
   
    {
        Debug.Log("Test1");

        Debug.Log(other.gameObject.tag);

        if (other.gameObject.tag == "TestL")
        {

            Debug.Log("Test2");

            agent.enabled = false;
            animator.SetBool("isRUn", false);
            other.GetComponent<BoxCollider>().enabled = false;
        }
      
    }

    void Fire(GameObject enemy)
    {
        GameObject newBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);

        Vector3 shootDir = (enemy.transform.position - firePoint.position).normalized;

        newBullet.GetComponent<Bullet>().Setup(shootDir, enemy.transform);

    }


    

    IEnumerator FireRoutine(GameObject enemy)
    {
        isActiveFire = true;

        yield return new WaitForSeconds(fireSpeed);

        if (enemies.Count > 0&&enemy!=null)
        {
            Fire(enemy);
            StartCoroutine(FireRoutine(enemy));
        }
        else
        {
            isActiveFire = false;
        }

    }





}
