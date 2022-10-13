using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    public Vector3 target;


    private void Start()
    {
        target = transform.localPosition;
    }


    private void Update()
    {
        transform.localPosition = Vector3.Lerp( transform.localPosition,target, 2*Time.deltaTime);
            
    }



}
