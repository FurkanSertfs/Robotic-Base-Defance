using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Colleactable : MonoBehaviour, ICollactable
{
    
    public CollectManager playerCollectManager;

    public CollectableObject.ObjectType objectType;

    float time = 0.3f;


    int collectObjeIndex;
    public void Collect()
    {
        collectObjeIndex = playerCollectManager.collectedObjects.Count;
        playerCollectManager.collectedObjects.Add(gameObject);
        transform.parent = playerCollectManager.transform;
        transform.rotation = playerCollectManager.transform.rotation;
        MovePlayer();
    }
    void MovePlayer() 
    {

        if (transform.position != new Vector3(playerCollectManager.collectableObjectSpawnPoint.position.x,
            playerCollectManager.collectableObjectSpawnPoint.position.y + 0.3f * (collectObjeIndex - 1),
            playerCollectManager.collectableObjectSpawnPoint.position.z))
        {
            transform.DOMove(new Vector3
           (playerCollectManager.collectableObjectSpawnPoint.position.x,
             playerCollectManager.collectableObjectSpawnPoint.position.y + 0.3f * (collectObjeIndex - 1),
             playerCollectManager.collectableObjectSpawnPoint.position.z
           ), time).OnComplete(() => MovePlayer());


            time -= 0.1f;
        }

    }





}
