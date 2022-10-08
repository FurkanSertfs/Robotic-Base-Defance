using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollactable<T> 
{

    public void Collect(CollectManager collectManager);
   
}
