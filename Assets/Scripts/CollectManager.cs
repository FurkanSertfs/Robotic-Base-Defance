using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectManager : MonoBehaviour
{
    public Transform collectableObjectSpawnPoint;

    public List<GameObject> collectedObjects = new List<GameObject>();

    

    [SerializeField]
   public int stackLimit;



    private void OnTriggerStay(Collider other)
    {

        if (other.GetComponent<Colleactable>() != null)
        {
            other.GetComponent<Colleactable>().Collect(this);

        }
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

    public void EditStack()
    {

        for (int i = 0; i < collectedObjects.Count; i++)
        {
        

            collectedObjects[i].transform.position = new Vector3
            (
                
            collectableObjectSpawnPoint.position.x,
            
            collectableObjectSpawnPoint.position.y +  (0.3f * i),
           
            collectableObjectSpawnPoint.position.z

            );
        }

    }


}
