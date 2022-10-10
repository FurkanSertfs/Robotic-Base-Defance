using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DropArea : MonoBehaviour, IDropable<CollectManager>
{
    
    public int stackLimit;

    [SerializeField]
    Colleactable.ObjectType dropAreaType;

    [SerializeField]
    MachineManager _machineManager;

    [SerializeField]
    Transform droppedObjectSpawnPoint;


    public List<GameObject> droppedObjects = new List<GameObject>();


    public void Drop(CollectManager playerCollectManager)
    {
            for (int i = playerCollectManager.collectedObjects.Count-1; i >= 0; i--)
            {
                

                if (droppedObjects.Count < stackLimit)
                {
                        if (playerCollectManager.collectedObjects[i].GetComponent<Colleactable>().objectType == dropAreaType)
                        {
                                playerCollectManager.collectedObjects[i].transform.parent = gameObject.transform;

                                playerCollectManager.collectedObjects[i].transform.rotation = droppedObjectSpawnPoint.transform.rotation;
                                playerCollectManager.collectedObjects[i].transform.localScale = droppedObjectSpawnPoint.transform.localScale;

                                playerCollectManager.collectedObjects[i].transform.DOMove(new Vector3

                                (

                                droppedObjectSpawnPoint.transform.position.x,

                                droppedObjectSpawnPoint.transform.position.y + (float)droppedObjects.Count / 3,

                                droppedObjectSpawnPoint.transform.position.z

                                ), 0.3f);
                                
                                droppedObjects.Add(playerCollectManager.collectedObjects[i]);

                                playerCollectManager.collectedObjects[i].transform.rotation = droppedObjectSpawnPoint.rotation;

                                Destroy(playerCollectManager.collectedObjects[i].GetComponent<Colleactable>());

                                playerCollectManager.collectedObjects.RemoveAt(i);
                        }
                }

                else
                {

                    break;

                }
            


    
            }


        StartCoroutine(Wait());



    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.45f);
        _machineManager.FillMachine();
    }
}
