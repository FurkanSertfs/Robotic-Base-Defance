using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngotMachineCanvas : MonoBehaviour
{
    [SerializeField]
    GameObject _camera;
    //[SerializeField]
    //MachineManager _machineManager;
    //[SerializeField]
    //Image _ingotBuild;

    void Update()
    {

        float distancex = _camera.transform.position.x - transform.position.x;
        float distancez = _camera.transform.position.z - transform.position.z;
        float targetAngle = Mathf.Atan2(distancex, distancez) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, targetAngle+180, 0f);
    }
}
