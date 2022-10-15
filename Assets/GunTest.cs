using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTest : MonoBehaviour
{
    public float fireSpeed;
  
    public Transform target;
   
    public Transform firePoint;
   
    public GameObject bullet;

    private void Start()
    {
        StartCoroutine(FireRoutine());
    }

    void Fire()
    {
        GameObject newBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);

        Vector3 shootDir = (target.position - firePoint.position).normalized;
        
        newBullet.GetComponent<Bullet>().Setup(shootDir, target);

    }


    

    IEnumerator FireRoutine( )
    {
        

        yield return new WaitForSeconds(fireSpeed);
        
        Fire();
            
        StartCoroutine(FireRoutine());
        

    }

}
