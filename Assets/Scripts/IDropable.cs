using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDropable<T>
{
    public void Drop(CollectManager collectManager);
    
}
