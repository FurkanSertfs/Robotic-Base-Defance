using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DropAreainCanvas : MonoBehaviour
{
    [SerializeField]
    Text _countText;
    [SerializeField]
    DropArea _dropArea;

    Vector3 _startScale;
    void Start()
    {
        _startScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        _countText.text = (_dropArea.droppedObjects.Count + "/" + _dropArea.stackLimit);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>()!=null)
        {
            //GetComponentInChildren<Canvas>().enabled = true;
            DOTween.To(() => new Vector3(0, 0, 0), x => GetComponentInChildren<Canvas>().transform.localScale = x, _startScale, 0.3f).SetEase(Ease.Linear);
        }
        
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            
            DOTween.To(() => _startScale, x => GetComponentInChildren<Canvas>().transform.localScale = x, new Vector3(0, 0, 0), 0.3f).SetEase(Ease.Linear);
            //GetComponentInChildren<Canvas>().enabled = false;
        }
        
    }
}
