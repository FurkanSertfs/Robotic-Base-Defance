using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CollectableObjectt",menuName ="Create New CollectableObject")]
public class CollectableObject : ScriptableObject
{
  
    public enum ObjectType { IronIngot=0,PlasticIngot=1,ScrapIron=2,ScrapPlastic=3 }


    public ObjectType objectType;
   

    
}
