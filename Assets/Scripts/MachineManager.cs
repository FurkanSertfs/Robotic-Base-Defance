using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class MachineManager : MonoBehaviour
{
    [SerializeField]
    public Transform pressPoint,machinePoint1, machinePoint2, machineDropPoint;

    public DropArea dropArea;

    public int machineLevel=1;

    public float machineSpeed;

    public bool isFull;

    [SerializeField]
    Light _fireLight;

    [SerializeField]
    GameObject _moltenIronPrefab;

    [SerializeField]
    PressMachine _pressMachine;

    [SerializeField]
    int _collectAreaCount = 5;

    private void Start()
    {
        machineSpeed = 2 / (Mathf.Pow(machineLevel, 1.2f) / 5 + 2);
    }
    public void FillMachine() 
    {
        
        if ( !isFull && dropArea.droppedObjects.Count>0 && _pressMachine._collectArea.collectableObjects.Count<_collectAreaCount)
        {
            
            isFull = true;

            CollectedObject dropped = dropArea.droppedObjects[dropArea.droppedObjects.Count - 1];

            dropped.collectedObject.transform.DOMove(machinePoint1.position, machineSpeed).SetEase(Ease.Linear).OnComplete(()=> 
                {
                    dropped.collectedObject.transform.DOMove(machinePoint2.position, machineSpeed).SetEase(Ease.Linear).OnComplete(() =>
                   
                    {
                        DOTween.To(() => (float) 0, x => _fireLight.intensity = x,5, machineSpeed).SetEase(Ease.Linear).OnComplete(()=> { DOTween.To(() => (float)5, x => _fireLight.intensity = x, 0, 0.25f).SetEase(Ease.Linear); });
                        
                        StartCoroutine(Processing(dropped));

                        

                    });
                    
                }
           );
            dropArea.droppedObjects.Remove(dropped);

        }
    }
    IEnumerator Processing(CollectedObject destroyObject) 
    {
        yield return new WaitForSeconds(0.5f);
        
        Destroy(destroyObject.collectedObject);

        GameObject _ironIngot = Instantiate(_moltenIronPrefab, machinePoint2.transform.position, machinePoint2.transform.rotation);
        _ironIngot.transform.rotation = pressPoint.transform.rotation;
        _ironIngot.transform.DOMove(pressPoint.position, machineSpeed).OnComplete(()=> 
        { 
            _pressMachine.Press(machineSpeed, _ironIngot, machineDropPoint);
        });

    }
}
