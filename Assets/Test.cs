using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    public Transform target;
    Sequence sequence;
    Vector3 temp;
    private void Start()
    {
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(target.position, 3).SetEase(Ease.Linear).OnComplete(() =>
        {
            Debug.Log("x");

        })
        );




    }



}
