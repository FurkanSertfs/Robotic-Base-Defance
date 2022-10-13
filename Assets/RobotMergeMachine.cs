using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RobotMergeMachine : MonoBehaviour
{

    public  bool isFull,isCanMarge,isMerging;
    [SerializeField]
    Transform modelPoint, modelPoint2, caryStartPoint, caryStartPoint2, caryEndPoint, caryEndPoint2, caryMergePoint, caryMergePoint2;
    
    [SerializeField]
    GameObject caryMachine, caryMachine2;

    [SerializeField]
    GameObject robotLevel1;

    [SerializeField]
    MergeMachineController mergeMachineController;

    public float timer;
    
    public void FillTheMachine(GameObject model)
    {
        if (!isFull)
        {
            isFull = true;
            model.transform.parent = modelPoint.transform;
            model.transform.DOMove(modelPoint.position, timer).OnComplete(()=>Production());
            model.transform.DOScale(new Vector3(1,1,1), timer).OnComplete(() => Production());
            model.transform.DORotateQuaternion(modelPoint.rotation, timer);

        }

    }

    public void FillTheMachine(GameObject model, GameObject model2)
    {
        if (!isFull)
        {
            isFull = true;

            model.transform.parent = modelPoint.transform;
            model.transform.DOMove(modelPoint.position, timer).OnComplete(() => Production(2));
            model.transform.DOScale(new Vector3(1, 1, 1), timer);
            model.transform.DORotateQuaternion(modelPoint.rotation, timer);

            model2.transform.parent = modelPoint2.transform;
            model2.transform.DOMove(modelPoint2.position, timer);
            model2.transform.DOScale(new Vector3(1, 1, 1), timer);
            model2.transform.DORotateQuaternion(modelPoint2.rotation, timer);



        }

    }







    void Production()
    {
        caryMachine.transform.DOMove(caryMergePoint.position, timer).OnComplete(()=> { isCanMarge = true; mergeMachineController.Merge(); });
    }


    void Production(int x)
    {
        caryMachine.transform.DOMove(caryMergePoint.position, timer).OnComplete(() => { isCanMarge = true; mergeMachineController.Merge(); });

        caryMachine2.transform.DOMove(caryMergePoint2.position, timer);
    }



    public void Merge()
    {
        if (!isMerging)
        {
            isMerging = true;
           
            caryMachine.transform.DOMove(caryEndPoint.position, timer).OnComplete(() =>
            {

                GameObject newRobot = Instantiate(robotLevel1, modelPoint.position, modelPoint.rotation);
                


            });
        }
      

    }

}
