using UnityEngine;

public class FindEnemy : MonoBehaviour
{
    [SerializeField]
    private EnemyAIManager enemyAIManager;

    [SerializeField]
    private AIManager aiManager;

    public bool isNotEnemy;

    [SerializeField]
    string enemyLayer;
    

    private void Start()
    {
        aiManager = GetComponent<SoldierAIManager>();
        
        enemyAIManager = GetComponent<EnemyAIManager>();

       
     
        

    }


    private void OnTriggerEnter(Collider other)
    {




        if (other.gameObject.layer == LayerMask.NameToLayer(enemyLayer))
        {
          
           

            if (other.GetComponentInParent<EnemyAIManager>())
            {

               aiManager.enemiesInRange.Add(other.gameObject);


            }

           else if (other.GetComponentInParent<SoldierAIManager>())
            {

                enemyAIManager.enemiesInRange.Add(other.gameObject);


            }
        }

       




    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(enemyLayer))
        {
          
            if (other.GetComponentInParent<EnemyAIManager>())
            {

                aiManager.enemiesInRange.Remove(other.gameObject);


            }

            else if (other.GetComponentInParent<SoldierAIManager>())
            {

                enemyAIManager.enemiesInRange.Remove(other.gameObject);


            }
        }

       
    }

}
