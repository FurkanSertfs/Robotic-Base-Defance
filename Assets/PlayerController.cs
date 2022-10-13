using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();

    private Animator animator;

    [SerializeField]
    float fireSpeed;

    [SerializeField]
    Transform firePoint;
    [SerializeField]
    GameObject bullet;


    private bool isActiveFire;

    private void Start()
    {
        animator= GetComponent<Animator>();
    }

    private void Update()
    {
        if (enemies.Count > 0)
        {
            //  animator.SetBool("isFire", true);


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
          //  animator.SetBool("isFire", false);
        }
   
    }

    void Fire(GameObject enemy)
    {
        GameObject newBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
        
        Vector3 shootDir = (enemy.transform.position - firePoint.position).normalized;
       
        newBullet.GetComponent<Bullet>().Setup(shootDir,enemy.transform);

    }



    IEnumerator FireRoutine(GameObject enemy)
    {
        isActiveFire = true;
       
        yield return new WaitForSeconds(fireSpeed);

        if (enemies.Count > 0)
        {
            Fire(enemy);
            StartCoroutine(FireRoutine(enemy));
        }
        else
        {
            isActiveFire = false;
        }
    
    }











    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SoldierPosition>() != null)
        {
            other.GetComponent<SoldierPosition>().Follow(GetComponent<PlayerSoldierManager>(), true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<SoldierPosition>() != null)
        {
            other.GetComponent<SoldierPosition>().Follow(GetComponent<PlayerSoldierManager>(), false);

        }
    }

}
