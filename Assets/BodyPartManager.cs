using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartManager : MonoBehaviour
{
    
    public enum BodyParts { Head=0, Body=1, LeftArm=2, RightArm=3, LeftLeg=4, RightLeg=5 }

    [NonReorderable]
    public List<BodyTypeHealth> bodyTypeHealths = new List<BodyTypeHealth>();

    int destroyedLeg;

   

    Animator animator;

    RuntimeAnimatorController[] animatorController;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();

       
    }

    public void DestrotBodyPart(BodyPartManager.BodyParts bodyPart)
    {
        for (int i = 0; i < bodyTypeHealths[(int)bodyPart].parts.Length; i++)
        {
            Destroy(bodyTypeHealths[(int)bodyPart].parts[i].gameObject);
        }

       

        if (bodyPart == BodyParts.LeftLeg || bodyPart == BodyParts.RightLeg)
        {
            animator.runtimeAnimatorController = animatorController[(int)bodyPart];
           
            destroyedLeg++;


        }

        if (destroyedLeg > 1)
        {
            Debug.Log("Yurumeyi Kapat");

            animator.runtimeAnimatorController = animatorController[6];
        }

        if (bodyPart == BodyParts.Head || bodyPart == BodyParts.Body)
        {
            Destroy(gameObject);

            Debug.Log("Listelerden Cikar");

        }




        }





}
[System.Serializable]
public class BodyTypeHealth
{
    public  BodyPartManager.BodyParts bodyPart;
    public float health;
    public GameObject targetPoint;
    public GameObject[] parts;
}




