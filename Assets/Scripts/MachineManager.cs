using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class MachineManager : MonoBehaviour
{
    [SerializeField]
    public Transform pressPoint, machineDropPoint,spawnResaourcesPoint;

    public DropArea dropArea;

    public int machineLevel=1;

    public float machineSpeed;

    public bool isFull;

    [SerializeField]
    Light _fireLight;

    [SerializeField]
    GameObject _productResources;

    [SerializeField]
    PressMachine _pressMachine;

    [SerializeField]
    int _collectAreaCount = 5;

    [NonReorderable]
    public List<InputObject> inputResources = new List<InputObject>();

    [SerializeField]
    private int totalCount=0;

    private void Start()
    {
        machineSpeed = 2 / (Mathf.Pow(machineLevel, 1.2f) / 5 + 2);
    }

    public void Control()
    {
        bool isCanProduction=true;
        totalCount = 0;
       
        for (int i = 0; i < inputResources.Count; i++)
        {
            if(inputResources[i].dropArea.droppedObjects.Count < inputResources[i].Count) 
            {
                isCanProduction = false;
            }
            totalCount += inputResources[i].Count;
          
        }

        if (isCanProduction)
        {
            FillMachine();
        }
    }


    //public void FillMachine() 
    //{
        
    //    if ( !isFull && dropArea.droppedObjects.Count > 0 && _pressMachine._collectArea.collectableObjects.Count<_collectAreaCount)
    //    {
            
    //        isFull = true;

    //        CollectedObject dropped = dropArea.droppedObjects[dropArea.droppedObjects.Count - 1];

    //        dropped.collectedObject.transform.DOMove(machinePoint1.position, machineSpeed).SetEase(Ease.Linear).OnComplete(()=> 
    //            {
    //                dropped.collectedObject.transform.DOMove(machinePoint2.position, machineSpeed).SetEase(Ease.Linear).OnComplete(() =>
                   
    //                {
    //                    DOTween.To(() => (float) 0, x => _fireLight.intensity = x,5, machineSpeed).SetEase(Ease.Linear).OnComplete(()=> { DOTween.To(() => (float)5, x => _fireLight.intensity = x, 0, 0.25f).SetEase(Ease.Linear); });
                        
    //                    StartCoroutine(Processing(dropped));

                        

    //                });
                    
    //            }
    //       );
    //        dropArea.droppedObjects.Remove(dropped);

    //    }
    //}

    void FillMachine()
    {

        if (!isFull &&  _pressMachine._collectArea.collectableObjects.Count < _collectAreaCount)
        {
            

            isFull = true;

            List<ProcessedObjectsList> processedObjectsLists = new List<ProcessedObjectsList>();

            for (int i = 0; i < inputResources.Count; i++)
            {
                processedObjectsLists.Add(new ProcessedObjectsList());

            }
        

           
            for (int i = 0; i < processedObjectsLists.Count; i++)
            {
              
                for (int j = 0; j < inputResources[i].Count; j++)
                {
                   
                    processedObjectsLists[i].processedObjects.Add(inputResources[i].dropArea.droppedObjects[dropArea.droppedObjects.Count - (j+1)]);
                }
               
            }

            StartCoroutine(Fill(processedObjectsLists,0,0));

          
        }
    }

    IEnumerator Fill(List<ProcessedObjectsList> list,int index,int x)
    {
       

       
        
        yield return new WaitForSeconds(machineSpeed);

        if (index < list[0].processedObjects.Count)
        {
           

            list[0].processedObjects[index].collectedObject.transform.DOMove(inputResources[0].machinePoint1.position, machineSpeed).SetEase(Ease.Linear).OnComplete(() =>
            {
              
                list[0].processedObjects[index].collectedObject.transform.DOMove(inputResources[0].machinePoint2.position, machineSpeed).SetEase(Ease.Linear).OnComplete(()=> 
                {

                    DOTween.To(() => (float)0, x => _fireLight.intensity = x, 5, machineSpeed).SetEase(Ease.Linear).OnComplete(() => { DOTween.To(() => (float)5, x => _fireLight.intensity = x, 0, 0.25f).SetEase(Ease.Linear); });

                });

            });
            inputResources[0].dropArea.droppedObjects.Remove(list[0].processedObjects[index]);
            x++;
           
        }

        if (list.Count > 1)
        {

            if (index < list[1].processedObjects.Count)
            {

          
                list[1].processedObjects[index].collectedObject.transform.DOMove(inputResources[1].machinePoint1.position, machineSpeed).SetEase(Ease.Linear).OnComplete(() =>
                {
                  list[1].processedObjects[index].collectedObject.transform.DOMove(inputResources[1].machinePoint2.position, machineSpeed).SetEase(Ease.Linear).OnComplete(()=> 
                  {

                      DOTween.To(() => (float)0, x => _fireLight.intensity = x, 5, machineSpeed).SetEase(Ease.Linear).OnComplete(() => { DOTween.To(() => (float)5, x => _fireLight.intensity = x, 0, 0.25f).SetEase(Ease.Linear); });

                  });

                });

                inputResources[1].dropArea.droppedObjects.Remove(list[1].processedObjects[index]);
                x++;
            }
        }

       // Makinedeki objeler temizlenebilir 

        if (x==totalCount)
        {
              StartCoroutine(Processing());
        }

        else
        {
            StartCoroutine(Fill(list, index + 1, x));
        }



    }



        IEnumerator Processing() 
        {
            yield return new WaitForSeconds(machineSpeed+1.4f);
        
            GameObject _ironIngot = Instantiate(_productResources, spawnResaourcesPoint.transform.position, spawnResaourcesPoint.transform.rotation);
            _ironIngot.transform.rotation = pressPoint.transform.rotation;
            _ironIngot.transform.localScale = pressPoint.transform.localScale;
            _ironIngot.transform.DOMove(pressPoint.position, machineSpeed).OnComplete(()=> 
            { 
            _pressMachine.Press(machineSpeed, _ironIngot, machineDropPoint);
            });

    }   
}
[System.Serializable]
public class ProcessedObjectsList
{
    public List<CollectedObject> processedObjects = new List<CollectedObject>();
  
}



[System.Serializable]
public class InputObject
{
    public DropArea dropArea;
    public int Count;
    public Transform machinePoint1, machinePoint2;
}
