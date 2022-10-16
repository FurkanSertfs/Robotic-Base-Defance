using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
   
    [SerializeField]
    BodyPartManager bodyPartManager;
  
    
    [SerializeField]
    public BodyPartManager.BodyParts bodyPart;

    public int id;

    public bool isTest;


    private void Start()
    {
        id = (int)bodyPart;
       
    }
    private void Update()
    {
       
    }



    public void Hit(float damage)
    {
        if (bodyPartManager.bodyTypeHealths.Count > id)
        {
            bodyPartManager.bodyTypeHealths[id].health -= damage;

            if (bodyPartManager.bodyTypeHealths[id].health <= 0)
            {
                bodyPartManager.DestrotBodyPart(bodyPart, id);

                
            }

          
        }
       
  
    
    }



}
