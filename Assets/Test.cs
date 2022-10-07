using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    public Transform target;
    Vector3 temp;


    public float health;
    public float damage;

    private void Start()
    {

        DOTween.To(() => health, x => health = x, health-damage, 0.5F).SetEase(Ease.Linear);

    }
  
  

}
