using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PressMachine : MonoBehaviour
{
    [SerializeField]
    public CollectArea _collectArea;

    [SerializeField]
    GameObject _pres1, _pres2;

    [SerializeField]
    Material[] _materials;

    Vector3 _startPosition1, _startPosition2;

    [SerializeField]
    Transform _endPosition1,_endPosition2;

    private void Start()
    {
        _startPosition1 = _pres1.transform.position;
        _startPosition2 = _pres2.transform.position;

    }
    public void Press(float time,GameObject _iron,Transform _machineDropPoint) 
    {
        _pres1.transform.DOMove(_endPosition1.position,time).OnComplete(()=> 
        {
            _pres2.transform.DOMove(_endPosition2.position, time).OnComplete(() => 
            {

                _pres1.transform.DOMove(_startPosition1, time);
                _pres2.transform.DOMove(_startPosition2, time);
                _iron.GetComponent<MeshRenderer>().materials = _materials;
                _iron.transform.DOMove(_machineDropPoint.position, time);
                _collectArea.AddObjectstoList(_iron);

            });

            
        });
    }
}
