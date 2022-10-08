using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Colleactable : MonoBehaviour, ICollactable<CollectManager>
{
    public enum ObjectType { RawIron = 0, RawPlastic = 1, IronIngot = 2, PlasticIngot = 3 };

    public ObjectType objectType;

    float time = 0.3f;


    int collectObjeIndex;
    public void Collect(CollectManager playerCollectManager)
    {
        if (playerCollectManager.collectedObjects.Count <playerCollectManager.stackLimit)
        {
            collectObjeIndex = playerCollectManager.collectedObjects.Count;
            playerCollectManager.collectedObjects.Add(gameObject);
            transform.parent = playerCollectManager.transform;
            transform.rotation = playerCollectManager.transform.rotation;
            MovePlayer(playerCollectManager);
        }

       
    }
    void MovePlayer(CollectManager playerCollectManager) 
    {

        if (transform.position != new Vector3(playerCollectManager.collectableObjectSpawnPoint.position.x,
            playerCollectManager.collectableObjectSpawnPoint.position.y + 0.3f * (collectObjeIndex - 1),
            playerCollectManager.collectableObjectSpawnPoint.position.z))
        {
            transform.DOMove(new Vector3
           (playerCollectManager.collectableObjectSpawnPoint.position.x,
             playerCollectManager.collectableObjectSpawnPoint.position.y + 0.3f * (collectObjeIndex - 1),
             playerCollectManager.collectableObjectSpawnPoint.position.z
           ), time).OnComplete(() => MovePlayer(playerCollectManager));


            time -= 0.1f;
        }

    }





}
