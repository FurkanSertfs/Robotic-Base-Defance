using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MergeMachineController : MonoBehaviour
{
    [SerializeField]
    RobotMergeMachine bodyMachine;

    [SerializeField]
    RobotMergeMachine[] robotMergeMachines;


    [SerializeField]
    Transform _buildMachineDoor;

    [SerializeField]
    Transform _doorFinish;

    Quaternion _doorStart;

    [SerializeField]
    GameObject _particalEfect;

    public List<ColactableBodyPart> colactableBodyParts;

    private void Start()
    {
        _doorStart = _buildMachineDoor.rotation;
        
    }
    public void OpenDoor() 
    {
        _buildMachineDoor.DORotateQuaternion(_doorFinish.rotation, 1).OnComplete(()=> { CloseDoor(); });
        _particalEfect.active = true;

    }
    public void CloseDoor() 
    {
        Debug.Log("closedoor");
        _buildMachineDoor.DORotateQuaternion(_doorStart, 1);
    }
    bool ControlMerge()
    {
        bool isCanMerge=true;

        for (int i = 0; i < robotMergeMachines.Length; i++)
        {
           if (!robotMergeMachines[i].isCanMarge)
            {
                isCanMerge = false;
            }
        }

        return isCanMerge;
    }

    public void Merge()
    {
        if (ControlMerge())
        {
            for (int i = 0; i < robotMergeMachines.Length; i++)
            {
                robotMergeMachines[i].robotPart.transform.parent = bodyMachine.robotPart.transform;
                
                if (robotMergeMachines[i].robotPart2!=null)
                {
                    robotMergeMachines[i].robotPart2.transform.parent = bodyMachine.robotPart.transform;
                }
               
            }


            StartCoroutine(Wait());



        }



    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.25f);
        bodyMachine.Merge();
    }

    public void Setup()
    {
        for (int i = 0; i < robotMergeMachines.Length; i++)
        {
            robotMergeMachines[i].Setup();
        }
    }


}
