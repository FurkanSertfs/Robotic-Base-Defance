using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    [SerializeField]
    Material _beltMaterial;

    [SerializeField]
    MachineManager _machineManager;

    public void Update()
    {
        if (_machineManager.isFull)
        {
            _beltMaterial.mainTextureOffset = new Vector2(_beltMaterial.mainTextureOffset.x, _beltMaterial.mainTextureOffset.y - 0.5f*_machineManager.machineLevel * Time.deltaTime);
        }

    }
}
