using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectManager : MonoBehaviour
{
    public Transform[] collectableObjectSpawnPoint;

    public List<CollectedObject> collectedObjects = new List<CollectedObject>();


    [SerializeField]
   public int stackLimit;


    private void OnTriggerStay(Collider other)
    {

       

        if (other.GetComponent<CollectArea>()!=null)
        {

            other.GetComponent<CollectArea>().CollectFromTopofList(this);

        }

        if (other.GetComponent<DropArea>() != null)
        {

            other.GetComponent<DropArea>().Drop(this);

            EditStack();


        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Colleactable>() != null)
        {
            other.GetComponent<Colleactable>().Collect(this);

        }
    }






    public void EditStack()
    {
        ;

        for (int i = 0; i < collectedObjects.Count; i++)
        {
        

            collectedObjects[i].collectedObject.transform.position = new Vector3
            (
                
            collectableObjectSpawnPoint[(int)collectedObjects[i].objectType].position.x,
            
            collectableObjectSpawnPoint[(int)collectedObjects[i].objectType].position.y +  (0.3f * i),
           
            collectableObjectSpawnPoint[(int)collectedObjects[i].objectType].position.z

            );
        }

    }



}
[System.Serializable]
public class CollectedObject
{
    public GameObject collectedObject;
    public Colleactable.ObjectType objectType;

    public CollectedObject(GameObject collectedObject, Colleactable.ObjectType objectType)
    {
        this.collectedObject = collectedObject;
        this.objectType = objectType;
    }
}
