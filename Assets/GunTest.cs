using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTest : MonoBehaviour
{
    public float fireSpeed;
  
    public Transform target;
   
    public Transform firePoint;

    public LayerMask layerMask;

    public GameObject bullet;

    private void Start()
    {
        StartCoroutine(FireRoutine());
    }

    void Fire()
    {
        RaycastHit hit;

       // GameObject newBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);

        //Vector3 shootDir = (target.position - firePoint.position).normalized;
        
        //var lookPos = target.position - firePoint.position;

        //var rotation = Quaternion.LookRotation(lookPos);

     //   firePoint.rotation = Quaternion.Slerp(firePoint.rotation, rotation, Time.deltaTime * 4);

       // newBullet.GetComponent<Bullet>().Setup(shootDir, target);

       

       

    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.forward), out hit, 50,layerMask))
        {
            
            Debug.Log(hit.collider.name);
            Debug.Log(hit);
        }

        Debug.DrawRay(firePoint.position, firePoint.TransformDirection(Vector3.forward) * 50, Color.red);
    }



    IEnumerator FireRoutine( )
    {
        

        yield return new WaitForSeconds(fireSpeed);
        
        Fire();
            
        StartCoroutine(FireRoutine());
        

    }

}
