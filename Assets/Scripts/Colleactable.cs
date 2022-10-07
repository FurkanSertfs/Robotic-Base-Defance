using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colleactable : MonoBehaviour, ICollactable
{
    
    public CollectManager playerCollectManager;

    public CollectableObject.ObjectType objectType;


    public void Collect()
    {
      

        playerCollectManager.collectedObjects.Add(gameObject);

        transform.parent = playerCollectManager.transform;
        transform.rotation = playerCollectManager.transform.rotation;


        transform.position =new Vector3
            (
            playerCollectManager.collectableObjectSpawnPoint.position.x,
            playerCollectManager.collectableObjectSpawnPoint.position.y +0.3f*(playerCollectManager.collectedObjects.Count-1),
            playerCollectManager.collectableObjectSpawnPoint.position.z
            
            );


        //Destroy(gameObject);


    }
}
