using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppenBodyPart : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SoldierAIManager>() == null)
        {
            Destroy(GetComponent<Rigidbody>());

            GetComponent<BoxCollider>().isTrigger = true;

            Destroy(this);
        }
    }
}
