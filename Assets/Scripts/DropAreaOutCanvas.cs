using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DropAreaOutCanvas : MonoBehaviour
{
    [SerializeField]
    Text _countText;

    [SerializeField]
    CollectArea _dropAreaOut;

    [SerializeField]
    Image _image;

    [SerializeField]
    MachineManager _machineManager;
    
    Vector3 _startScale, _stopScale;
    // Start is called before the first frame update
    void Start()
    {
        _startScale = _image.transform.localScale;
        _stopScale = new Vector3(0.0373261832f, 0.0245571584f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        _countText.text = (_dropAreaOut.collectableObjects.Count + "/" + _machineManager._collectAreaCount);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
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
