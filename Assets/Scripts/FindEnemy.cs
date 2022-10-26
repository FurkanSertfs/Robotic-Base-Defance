using UnityEngine;

public class FindEnemy : MonoBehaviour
{
    [SerializeField]
    private EnemyAIManager enemyAIManager;

    [SerializeField]
    private AIManager aiManager;

    public bool isNotEnemy;

    [SerializeField]
    LayerMask enemyLayer;

    private void Start()
    {
        aiManager = GetComponent<SoldierAIManager>();
        
        enemyAIManager = GetComponent<EnemyAIManager>();


    }


    private void OnTriggerEnter(Collider other)
    {
        if (isNotEnemy)
        {
            Debug.Log(LayerMask.LayerToName(other.gameObject.layer)+ " " + other.gameObject.name);
          
        }
        

        if(other.gameObject.layer == enemyLayer)
        {
            if (other.GetComponentInParent<EnemyAIManager>())
            {

               aiManager.enemiesInRange.Add(other.gameObject);


            }

           else if (other.TryGetComponent<SoldierAIManager>(out SoldierAIManager soldierEnemy))
            {

                enemyAIManager.enemiesInRange.Add(other.gameObject);


            }
        }

       




    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == enemyLayer)
        {
          
            if (other.GetComponentInParent<EnemyAIManager>())
            {

                aiManager.enemiesInRange.Remove(other.gameObject);


            }

            else if (other.TryGetComponent<SoldierAIManager>(out SoldierAIManager soldierEnemy))
            {

                enemyAIManager.enemiesInRange.Remove(other.gameObject);


            }
        }

       
    }

}
