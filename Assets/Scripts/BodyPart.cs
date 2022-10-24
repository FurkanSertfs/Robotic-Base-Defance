using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField]
    BodyPartManager bodyPartManager;
    
    public int level;
    
    public BodyPartManager.BodyParts bodyPart;

    public int id;
    
   

    public bool isWhite;


    private void Start()
    {
        id = (int)bodyPart;

      
    }
    


}

