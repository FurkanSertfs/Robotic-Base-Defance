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

    [SerializeField]
    Image _image;

    Vector3 _startScale,_stopScale;
    void Start()
    {
        _startScale = _image.transform.localScale;
        _stopScale = new Vector3(0.0373261832f, 0.0245571584f, 1);
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
            
            DOTween.To(() => _startScale, x => _image.transform.localScale = x, _stopScale, 0.3f).SetEase(Ease.Linear);
        }
        
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            
            DOTween.To(() => _stopScale
            , x => _image.transform.localScale = x, _startScale, 0.3f).SetEase(Ease.Linear);
            
        }
        
    }
}
