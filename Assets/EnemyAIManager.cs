using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyAIManager : MonoBehaviour
{
    public Transform target;

   
    public List<GameObject> enemies = new List<GameObject>();

    private Animator animator;

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

    public Image healthBar,healthBarBG;

    public bool isHaveTarget;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
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
        healthBar.fillAmount = (float)(health / 100.0f);

      

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


       
    }


    void Fire(GameObject enemy)
    {
        Transform newTarget = enemy.transform;
        BodyPartManager bodyPartManager;
        bodyPartManager = enemy.GetComponent<BodyPartManager>();
        
        int target = Random.Range(0, bodyPartManager.bodyTypeHealths.Count);
        if (enemy != null && bodyPartManager.bodyTypeHealths[target]!=null)
        {
            Debug.Log(bodyPartManager.bodyTypeHealths[target]);
            newTarget = bodyPartManager.bodyTypeHealths[target].targetPoint.transform;

            GameObject newBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);

            Vector3 shootDir = (newTarget.position - firePoint.position).normalized;

            newBullet.GetComponent<Bullet>().Setup(shootDir, newTarget);

        }
       

    }



    IEnumerator FireRoutine(GameObject enemy)
    {
        isActiveFire = true;

        yield return new WaitForSeconds(fireSpeed);

        if (enemies.Count > 0 && enemy != null)
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
