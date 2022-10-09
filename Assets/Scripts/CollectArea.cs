using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectArea : MonoBehaviour
{
    [SerializeField]
    MachineManager _machineManager;

    public List<GameObject> collectableObjects;

    public void AddObjectstoList(GameObject _iron) 
    {   
        
        
        _iron.transform.DOMoveY(_machineManager.machineDropPoint.position.y+ 0.3f * collectableObjects.Count, 1).OnComplete(()=> 
        { 

            collectableObjects.Add(_iron);
            _machineManager.isFull = false;
            _machineManager.FillMachine();

        });
     
    }
    public void CollectFromTopofList(CollectManager playerCollectManager) 
    {
        if (collectableObjects.Count>0)
        {
            collectableObjects[collectableObjects.Count - 1].GetComponent<Colleactable>().Collect(playerCollectManager);
            collectableObjects.RemoveAt(collectableObjects.Count - 1);
            _machineManager.FillMachine();
        }
        
    }
}
