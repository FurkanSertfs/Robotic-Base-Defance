using UnityEngine;

public class FindEnemy : MonoBehaviour
{
    [SerializeField]
    private EnemyAIManager enemyAIManager;

    [SerializeField]
    private AIManager aiManager;

  

    [SerializeField]
    bool isEnemy;

    private void Start()
    {
        aiManager = GetComponentInParent<SoldierAIManager>();
        
        enemyAIManager = GetComponentInParent<EnemyAIManager>();


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyAIManager>(out EnemyAIManager enemy)&&!isEnemy)
        {
          
            aiManager.enemiesInRange.Add(other.gameObject);

          
        }

        if (other.GetComponentInParent<SoldierAIManager>() && isEnemy)
        {

            enemyAIManager.enemiesInRange.Add(other.gameObject);


        }




    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<EnemyAIManager>(out EnemyAIManager enemy)&&!isEnemy)
        {

            aiManager.enemiesInRange.Remove(other.gameObject);


        }


        if (other.TryGetComponent<SoldierAIManager>(out SoldierAIManager solider) && isEnemy)
        {

            enemyAIManager.enemiesInRange.Remove(other.gameObject);


        }
    }

}
