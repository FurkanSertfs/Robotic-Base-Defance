using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierPosition : MonoBehaviour, IFollowable<PlayerSoldierManager,bool>
{
    public AIManager aIManager;

    public Transform defancePosition;

    [SerializeField]
    Image followBar;

    bool isThere;

    public void Follow(PlayerSoldierManager playerCollectManager,bool isThere)
    {
        this.isThere = isThere;
      

        if (isThere&&aIManager!=null)
        {
            StartCoroutine(FilltheBar(playerCollectManager));

        }

    }





    IEnumerator FilltheBar(PlayerSoldierManager playerSoldierManager)
    {
        followBar.fillAmount += 0.05f;
      
        
        yield return new WaitForSeconds(0.05f);
      
        if (isThere& followBar.fillAmount<1)
        {
            StartCoroutine(FilltheBar(playerSoldierManager));
        }
       
        else if (!isThere)
        {
            followBar.fillAmount = 0;
        }

        else if (followBar.fillAmount >= 1)
        {
            followBar.fillAmount = 0;

            for (int i = 0; i < playerSoldierManager.soldierTransforms.Count; i++)
            {
                if (!playerSoldierManager.soldierTransforms[i].isFull)
                {
                    aIManager.target = playerSoldierManager.soldierTransforms[i].soldierTransform;
                    aIManager.id = i;
                  
                    aIManager.player = playerSoldierManager.gameObject;
                  
                    playerSoldierManager.soldierTransforms[i].isFull = true;

                    aIManager = null;
                   
                    break;
                }

            }

           
       
        }

    
    }



}
