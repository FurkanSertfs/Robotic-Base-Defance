using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSpawnAnim : MonoBehaviour
{
   public void CloseAnim()
    {
        GetComponent<Animator>().SetBool("isSpawn", false);

        BaseDefanceManager.baseDefanceManager.AddSoldier(GetComponentInParent<AIManager>());

    }
}
