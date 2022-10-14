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
    RobotMergeMachine _robotMergeMachine;

    [SerializeField]
    Transform droppedObjectSpawnPoint;

    [SerializeField]
   public int minCount;

    public enum DropAreaType {Machine , MergeMachine }

    [SerializeField]
    DropAreaType dropArea;


    public List<CollectedObject> droppedObjects = new List<CollectedObject>();


    public void Drop(CollectManager playerCollectManager)
    {
            for (int i = playerCollectManager.collectedObjects.Count-1; i >= 0; i--)
            {
                

                if (droppedObjects.Count < stackLimit)
                {
                        if (playerCollectManager.collectedObjects[i].collectedObject.GetComponent<Colleactable>().objectType == dropAreaType)
                        {
                                playerCollectManager.collectedObjects[i].collectedObject.transform.parent = gameObject.transform;

                                playerCollectManager.collectedObjects[i].collectedObject.transform.rotation = droppedObjectSpawnPoint.transform.rotation;
                              
                                if(dropArea == DropAreaType.Machine)
                                playerCollectManager.collectedObjects[i].collectedObject.transform.localScale = droppedObjectSpawnPoint.transform.localScale;

                                playerCollectManager.collectedObjects[i].collectedObject.transform.DOMove(new Vector3

                                (

                                droppedObjectSpawnPoint.transform.position.x,

                                droppedObjectSpawnPoint.transform.position.y + (float)droppedObjects.Count / 3,

                                droppedObjectSpawnPoint.transform.position.z

                                ), 0.3f);
                                
                                droppedObjects.Add(playerCollectManager.collectedObjects[i]);

                                playerCollectManager.collectedObjects[i].collectedObject.transform.rotation = droppedObjectSpawnPoint.rotation;

                                Destroy(playerCollectManager.collectedObjects[i].collectedObject.GetComponent<Colleactable>());

                                playerCollectManager.collectedObjects.RemoveAt(i);
                        }
                }

                else
                {

                    break;

                }
            


    
            }

        if (dropArea == DropAreaType.Machine)
        {
            StartCoroutine(SetupMachine());
        }
        
        else if (dropArea == DropAreaType.MergeMachine)

        {
            StartCoroutine(SetupMergeMachine());
        }
       



    }

    public IEnumerator SetupMergeMachine()
    {
      
     
        yield return new WaitForSeconds(0.45f);
        if (droppedObjects.Count >= minCount&&!_robotMergeMachine.isFull)
        {
           

            if (minCount > 1)
            {
                
                _robotMergeMachine.FillTheMachine(droppedObjects[droppedObjects.Count - 1].collectedObject, droppedObjects[droppedObjects.Count - 2].collectedObject);
                droppedObjects.RemoveAt(droppedObjects.Count - 1);
                droppedObjects.RemoveAt(droppedObjects.Count - 1);
            }

            else if(minCount <= 1)
            {
               
                _robotMergeMachine.FillTheMachine(droppedObjects[droppedObjects.Count - 1].collectedObject);
                
                droppedObjects.RemoveAt(droppedObjects.Count - 1);
            }
          
        }
    }
    


  public  IEnumerator SetupMachine()
    {
        yield return new WaitForSeconds(0.45f);
        _machineManager.FillMachine();
    }
}
