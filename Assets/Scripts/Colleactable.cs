using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Colleactable : MonoBehaviour, ICollactable<CollectManager>
{
    public enum ObjectType { RawIron = 0, RawCopper = 1, IronIngot = 2, CopperIngot = 3, Head=4, Body=5, Arm=6, Leg=7 };

    public ObjectType objectType;

    float time = 0.2f;

    bool isCollecting;


    int collectObjeIndex;
    public void Collect(CollectManager playerCollectManager)
    {
        if (playerCollectManager.collectedObjects.Count <playerCollectManager.stackLimit && !isCollecting)
        {
           

            isCollecting = true;
            collectObjeIndex = playerCollectManager.collectedObjects.Count;
            playerCollectManager.collectedObjects.Add(new CollectedObject(gameObject,objectType));
            transform.parent = playerCollectManager.transform;
            transform.rotation = playerCollectManager.collectableObjectSpawnPoint[(int)objectType].transform.rotation;
            MovePlayer(playerCollectManager);
        }

       
    }
    void MovePlayer(CollectManager playerCollectManager) 
    {

        if (transform.position != new Vector3(playerCollectManager.collectableObjectSpawnPoint[(int)objectType].position.x,
            playerCollectManager.collectableObjectSpawnPoint[(int)objectType].position.y + 0.3f * collectObjeIndex,
            playerCollectManager.collectableObjectSpawnPoint[(int)objectType].position.z))
        {
            transform.DOMove(new Vector3
           (playerCollectManager.collectableObjectSpawnPoint[(int)objectType].position.x,
             playerCollectManager.collectableObjectSpawnPoint[(int)objectType].position.y + 0.3f *collectObjeIndex,
             playerCollectManager.collectableObjectSpawnPoint[(int)objectType].position.z
           ), time).OnComplete(() => MovePlayer(playerCollectManager));


            time -= 0.075f;
        }
      

    }





}
