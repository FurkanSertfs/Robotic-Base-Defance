using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamereFollowPlayer : MonoBehaviour
{

    [SerializeField]
    Transform target;

    [SerializeField]
    Vector3 offset;

    [SerializeField]
    float speed;

    [SerializeField]
    bool followRotate;

    

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, speed);

     
    }



}
