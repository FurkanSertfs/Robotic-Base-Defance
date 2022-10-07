using Simofun.Unity.Genre.ArcadeIdle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    #region Fields
    Animator animator;
 
    [SerializeField]
     ParticleSystem particleSystem;

    [SerializeField]
     AudioSource audioSource;

    float deacticeTimer;

    IMove move;

   
    #endregion
    protected virtual void Awake()
    {
        this.animator = this.GetComponentInChildren<Animator>() ?? this.GetComponent<Animator>();
        this.move = this.GetComponent<IMove>();
    }
    void Start()
    {
      
    }

   
    void Update()
    {
        if (move.Speed > 0.2F)
        {
            animator.SetBool("isRun",true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }
       
    }
}
