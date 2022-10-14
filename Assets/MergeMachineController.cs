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

            bodyMachine.Merge();
        }



    }

    public void Setup()
    {
        for (int i = 0; i < robotMergeMachines.Length; i++)
        {
            robotMergeMachines[i].Setup();
        }
    }


}
