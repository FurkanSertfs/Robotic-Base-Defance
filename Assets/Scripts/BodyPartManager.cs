using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartManager : MonoBehaviour
{
    
    public enum BodyParts { Head=0, Body=1, LeftArm=2, RightArm=3, LeftLeg=4, RightLeg=5 }

    [NonReorderable]
    public List<BodyTypeHealth> bodyTypeHealths = new List<BodyTypeHealth>();

   public int destroyedLeg;

   

    Animator animator;
    [SerializeField]
    RuntimeAnimatorController[] animatorController;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

  

    public void DestrotBodyPart(BodyPartManager.BodyParts bodyPart, int id)
    {
        for (int i = 0; i < bodyTypeHealths[id].parts.Length; i++)
        {
            Destroy(bodyTypeHealths[id].parts[i].gameObject);
           
        }


        bodyTypeHealths.RemoveAt(id);

        for (int i = 0; i < bodyTypeHealths.Count; i++)
        {
            for (int j = 0; j < bodyTypeHealths[i].parts.Length; j++)
            {
             
                bodyTypeHealths[i].parts[j].GetComponent<BodyPart>().id = i;
               
            }

              
            
        }



        if (bodyPart == BodyParts.LeftLeg || bodyPart == BodyParts.RightLeg)
        {
            
            animator.runtimeAnimatorController = animatorController[(int)bodyPart];
           
            destroyedLeg++;

            if (destroyedLeg == 2)
            {
                animator.runtimeAnimatorController = animatorController[6];

            }

        }

    

        if (bodyPart == BodyParts.Head || bodyPart == BodyParts.Body)
        {
            Destroy(gameObject);

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




