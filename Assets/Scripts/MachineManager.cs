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
    public void FillMachine() 
    {
        
        if ( !isFull && dropArea.droppedObjects.Count>0 && _pressMachine._collectArea.collectableObjects.Count<_collectAreaCount)
        {

            isFull = true;

            GameObject dropped = dropArea.droppedObjects[dropArea.droppedObjects.Count - 1];

            dropped.transform.DOMove(machinePoint1.position, 1).SetEase(Ease.Linear).OnComplete(()=> 
                {
                    dropped.transform.DOMove(machinePoint2.position, 1).SetEase(Ease.Linear).OnComplete(() =>
                   
                    {
                        DOTween.To(() => (float) 0, x => _fireLight.intensity = x,5, 0.25f).SetEase(Ease.Linear).OnComplete(()=> { DOTween.To(() => (float)5, x => _fireLight.intensity = x, 0, 0.25f).SetEase(Ease.Linear); });
                        
                        StartCoroutine(Processing(dropped));

                        

                    });
                    
                }
           );
            dropArea.droppedObjects.Remove(dropped);

        }
    }
    IEnumerator Processing(GameObject destroyObject) 
    {
        yield return new WaitForSeconds(0.5f);
        
        Destroy(destroyObject);

        GameObject _ironIngot = Instantiate(_moltenIronPrefab, machinePoint2.transform.position, machinePoint2.transform.rotation);
        _ironIngot.transform.rotation = pressPoint.transform.rotation;
        _ironIngot.transform.DOMove(pressPoint.position, 0.5f).OnComplete(()=> 
        { 
            _pressMachine.Press(0.4f, _ironIngot, machineDropPoint);
        });

    }
}
