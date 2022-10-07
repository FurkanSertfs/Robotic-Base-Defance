using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    public Transform collectableObjectSpawnPoint;

    public List<GameObject> collectedObjects = new List<GameObject>();

    

    [SerializeField]
    int stackLimit;



    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Colleactable>() != null)
        {
      
            other.GetComponent<Colleactable>().playerCollectManager = this;
           
            other.GetComponent<Colleactable>().Collect();


        }

        if (other.GetComponent<DropArea>() != null)
        {
            other.GetComponent<DropArea>().playerCollectManager = this;

            other.GetComponent<DropArea>().Drop();

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
