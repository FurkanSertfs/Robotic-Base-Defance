using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RobotMergeMachine : MonoBehaviour
{
  
    public  bool isFull,isCanMarge,isMerging;

    public GameObject robotPart, robotPart2;

    [SerializeField]
    Transform modelPoint, modelPoint2, robotSpawnPoint;
       
    [SerializeField]
    GameObject robotLevel1;

    [SerializeField]
    MergeMachineController mergeMachineController;

    [SerializeField]
    DropArea dropArea;


    public float timer;
    

    public void FillTheMachine(GameObject model)
    {
        if (!isFull)
        {
          
            isFull = true;

            robotPart = model;

            model.transform.parent = modelPoint.transform;
          
            model.transform.DOMove(modelPoint.position, timer).OnComplete(()=>Production());
         
            model.transform.DOScale(new Vector3(1,1,1), timer);
          
            model.transform.DORotateQuaternion(modelPoint.rotation, timer);

            mergeMachineController.colactableBodyParts[model.GetComponent<ColactableBodyPart>().Id] = robotPart.GetComponent<ColactableBodyPart>();


        }

    }

    public void FillTheMachine(GameObject model, GameObject model2)
    {
        if (!isFull)
        {
            isFull = true;

            robotPart = model;
            robotPart2 = model2;
            model.transform.parent = modelPoint.transform;
            model.transform.DOMove(modelPoint.position, timer).OnComplete(() => Production(2));
            model.transform.DOScale(new Vector3(1, 1, 1), timer);
            model.transform.DORotateQuaternion(modelPoint.rotation, timer);

            mergeMachineController.colactableBodyParts[model.GetComponent<ColactableBodyPart>().Id] = model.GetComponent<ColactableBodyPart>();
            mergeMachineController.colactableBodyParts[model2.GetComponent<ColactableBodyPart>().Id+1] = model2.GetComponent<ColactableBodyPart>();

            model2.transform.parent = modelPoint2.transform;
            model2.transform.DOMove(modelPoint2.position, timer);
            model2.transform.DOScale(new Vector3(1, 1, 1), timer);
            model2.transform.DORotateQuaternion(modelPoint2.rotation, timer);



        }

    }



    void Production()
    {
        isCanMarge = true; mergeMachineController.Merge();
    }


    void Production(int x)
    {
        isCanMarge = true; mergeMachineController.Merge();
    }

    public void Setup()
    {


             isFull = false;

             isMerging = false;

             isCanMarge = false;

             StartCoroutine(dropArea.SetupMergeMachine());      
    }

    public void CreateRobot()
    {
        Destroy(robotPart);

        GameObject newRobot = Instantiate(robotLevel1, robotSpawnPoint.position, robotSpawnPoint.rotation);

        newRobot.GetComponent<BodyPartManager>().bodyTypeHealths[0].parts[0].GetComponent<MeshFilter>().mesh = mergeMachineController.colactableBodyParts[0].partObject[0];

        for (int j = 0; j < 6; j++)
        {

            for (int i = 0; i < newRobot.GetComponent<BodyPartManager>().bodyTypeHealths[j].parts.Length; i++)
            {

                newRobot.GetComponent<BodyPartManager>().bodyTypeHealths[j].parts[i].GetComponent<MeshFilter>().mesh = mergeMachineController.colactableBodyParts[j].partObject[i];

                newRobot.GetComponent<BodyPartManager>().bodyTypeHealths[j].parts[i].GetComponent<BodyPart>().level = mergeMachineController.colactableBodyParts[j].level;
            }


    }

        mergeMachineController.OpenDoor();
        mergeMachineController.Setup();

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        CreateRobot();
    }


    public void Merge()
    {
        if (!isMerging)
        {
            isMerging = true;
           
            StartCoroutine(Wait());
        }
      

    }

}
