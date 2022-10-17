using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PressMachine : MonoBehaviour
{
    [SerializeField]
    public CollectArea _collectArea;



    [SerializeField]
    Material[] _materials;

    Vector3 _startPosition;

    [SerializeField]
    Transform _endPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }
    public void Press(float time,GameObject _iron,Transform _machineDropPoint) 
    {
        transform.DOMove(_endPosition.position,time).OnComplete(()=> 
        {
            transform.DOMove(_startPosition, time);
            for (int i = 0; i < _iron.GetComponent<MeshRenderer>().materials.Length; i++)
            {
                _iron.GetComponent<MeshRenderer>().materials[i] = _materials[i];
            }

            _iron.transform.DOMove(_machineDropPoint.position, time);
            _collectArea.AddObjectstoList(_iron);
            
        });
    }
}
