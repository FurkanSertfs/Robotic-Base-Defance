using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
   
    [SerializeField]
    BodyPartManager bodyPartManager;
  
    
    [SerializeField]
    BodyPartManager.BodyParts bodyPart;

  

    public void Hit(float damage)
    {
        bodyPartManager.bodyTypeHealths[(int)bodyPart].health -= damage;
      
        if (bodyPartManager.bodyTypeHealths[(int)bodyPart].health <= 0)
        {
            bodyPartManager.DestrotBodyPart(bodyPart);
        }
  
    
    }



}
