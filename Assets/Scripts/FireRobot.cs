using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRobot : MonoBehaviour
{
    public Transform point;
    public GameObject bullet;
    public GameObject a;


    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            GameObject bul = Instantiate(bullet, point.position, point.rotation);

            bul.GetComponent<Rigidbody>().AddForce(Vector3.forward*500);

            
        }
    }
}
