using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    AIManager aIManager;

    [SerializeField]
    GameObject bullet;
    [SerializeField]
    Transform firePoint;


    void Fire(GameObject enemy)
    {
        GameObject newBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);


        Vector3 shootDir = (enemy.transform.position - firePoint.position).normalized;

        newBullet.GetComponent<Bullet>().Setup(shootDir, enemy.transform);
        newBullet.GetComponent<Bullet>().name = "pþa";
    }

   public IEnumerator FireRoutine(GameObject enemy, float fireSpeed)
    {

        aIManager.isActiveFire = true;

        yield return new WaitForSeconds(fireSpeed);

        if (enemy != null && aIManager.enemies.Count>0 && aIManager.enemies[0]==enemy)
        {

            Fire(enemy);
            aIManager.isActiveFire = false;
         //   StartCoroutine(FireRoutine(enemy, fireSpeed));
        }

        else
        {
            aIManager.isActiveFire = false;


        }

        aIManager.isActiveFire = false;

    }
}
