using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BodyPartManager : MonoBehaviour
{
    public enum BodyParts { Head=0, Body=1, LeftArm=2, RightArm=3, LeftLeg=4, RightLeg=5 }

    [NonReorderable]
    public List<BodyTypeHealth> bodyTypeHealths = new List<BodyTypeHealth>();

    public int destroyedLeg;

    Animator animator;
    
    [SerializeField]
    RuntimeAnimatorController[] animatorController;

    public int animatorControllerId;

    [SerializeField]
    GameObject[] oils,bodyParts;
    [SerializeField]
    GameObject trailRenderer;

    [HideInInspector]
    public  bool isHandDestroyed,noLeg;

    [SerializeField]
    Transform[] gunPosition;

    

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void DestrotBodyPart(int index)
    {
        if (index == 2 || index == 3)
        {
            isHandDestroyed = true;
         
            if (index == 3)
            {
                GetComponent<SoldierAIManager>().ChangeGunPosition(gunPosition[0]);
            }
        }

        if (index == 4)
        {
            oils[0].SetActive(true);
            trailRenderer.SetActive(true);
            GetComponent<NavMeshAgent>().speed -= 2;

        }

        if (index == 5)
        {
            oils[1].SetActive(true);
            trailRenderer.SetActive(true);
            GetComponent<NavMeshAgent>().speed -= 2;

        }




        animatorControllerId += bodyTypeHealths[index].animatorID;
      
        bodyTypeHealths[index].isDestroyed = true;

        GameObject newBodyPart = Instantiate(bodyParts[index], bodyTypeHealths[index].parts[0].transform.position, Quaternion.identity);

        newBodyPart.transform.parent = BaseDefanceManager.baseDefanceManager.gameObject.transform;


        for (int i = 0; i < bodyTypeHealths[index].parts.Length; i++)
        {
            Destroy(bodyTypeHealths[index].parts[i].gameObject);
           
        }

        animator.runtimeAnimatorController = animatorController[animatorControllerId];
       
        animator = GetComponentInChildren<Animator>();


        Debug.Log(animatorControllerId);


        if (animatorControllerId == 12 || animatorControllerId == 13 || animatorControllerId == 14)
        {
            noLeg = true;
            GetComponent<NavMeshAgent>().speed = 3;

            if (animatorControllerId == 14)
            {
                GetComponent<SoldierAIManager>().ChangeGunPosition(gunPosition[1]);
            }
            else
            {
                GetComponent<SoldierAIManager>().ChangeGunPosition(gunPosition[2]);
            }
        }

      


       

    }





}
[System.Serializable]
public class BodyTypeHealth
{
    public  BodyPartManager.BodyParts bodyPart;
    
    public GameObject[] parts;
   
    public int animatorID;

    public bool isDestroyed;
}




