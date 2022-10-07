using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour, IDropable
{
    [HideInInspector]
    public CollectManager playerCollectManager;

    public int stackLimit;

    [SerializeField]
    CollectableObject.ObjectType dropAreaType;

    [SerializeField]
    Transform droppedObjectSpawnPoint;


    public List<GameObject> droppedObjects = new List<GameObject>();


    public void Drop()
    {
            for (int i = playerCollectManager.collectedObjects.Count-1; i >= 0; i--)
            {

                if (droppedObjects.Count < stackLimit)
                {
                        if (playerCollectManager.collectedObjects[i].GetComponent<Colleactable>().objectType == dropAreaType)
                        {
                    playerCollectManager.collectedObjects[i].transform.parent = gameObject.transform;


                    playerCollectManager.collectedObjects[i].transform.position = new Vector3

                (

                droppedObjectSpawnPoint.transform.position.x,

                droppedObjectSpawnPoint.transform.position.y + (float)droppedObjects.Count / 3,

                droppedObjectSpawnPoint.transform.position.z

                );

                    playerCollectManager.collectedObjects[i].transform.rotation = droppedObjectSpawnPoint.rotation;

                    droppedObjects.Add(playerCollectManager.collectedObjects[i]);

                            Destroy(playerCollectManager.collectedObjects[i].GetComponent<Colleactable>());

                            playerCollectManager.collectedObjects.RemoveAt(i);
                            


                        

                    }


                }

                else
                {

                    break;

                }




            }

     

     
    }
}
