using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
   
    [SerializeField]
    BodyPartManager bodyPartManager;
    
  
    public int level;
    
    [SerializeField]
    public BodyPartManager.BodyParts bodyPart;

    public int id;
    
    public bool isWhite;

 
  //  List<RobotMaterials> robotMaterials;


    private void Start()
    {
        id = (int)bodyPart;

       
      //  GetComponent<MeshRenderer>().materials = robotMaterials[level-1].materials;

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
[System.Serializable]
public class RobotMaterials
{
  public  Material[] materials;
}
