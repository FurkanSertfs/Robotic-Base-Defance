using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyAIManager : MonoBehaviour
{
    public Transform target, testTarget;
    [SerializeField]
    LayerMask layerMask;
   
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

    void DrawRaycast(int target)
    {
      


        if (enemies[0].GetComponent<BodyPartManager>().bodyTypeHealths.Count> target)
        {
            Transform a = enemies[0].GetComponent<BodyPartManager>().bodyTypeHealths[target].targetPoint.transform;

            var lookPos = a.position - firePoint.position;

            var rotation = Quaternion.LookRotation(lookPos);

            firePoint.rotation = Quaternion.Slerp(firePoint.rotation, rotation, Time.deltaTime * 4);

            firePoint.transform.LookAt(a);

            RaycastHit hit;

            if (Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.forward), out hit, 50, layerMask))
            {
                Debug.Log("Vurdum 0 ");

                if (hit.collider.GetComponent<BodyPart>() != null)
                {
                    hit.collider.GetComponent<BodyPart>().Hit(6);

                    Debug.Log("Vurdum");

                }

            }
        }
        else
        {
            Debug.Log("hATA");

        }






    }

    void Fire(GameObject enemy)
    {

        Debug.Log("Fire basladi");

        Transform newTarget = enemy.transform;

        BodyPartManager bodyPartManager;

        bodyPartManager = enemy.GetComponent<BodyPartManager>();

        int target = Random.Range(0, bodyPartManager.bodyTypeHealths.Count);

      
       
        DrawRaycast(target);

       

        if (enemy != null)
        {

            if (bodyPartManager.bodyTypeHealths.Count>target)
            {
                
                    newTarget = bodyPartManager.bodyTypeHealths[target].targetPoint.transform;

                    GameObject newBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);

                    Vector3 shootDir = (newTarget.position - firePoint.position).normalized;

                    newBullet.GetComponent<Bullet>().Setup(shootDir, newTarget);

                
            }
            else
            {
               
            }

           


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
