using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemy : MonoBehaviour
{
    [SerializeField]
    private EnemyAIManager enemyAIManager;

    [SerializeField]
    private AIManager aiManager;

    [SerializeField]
    Component findComponent;

    [SerializeField]
    bool isEnemy;

    private void Start()
    {
        aiManager = GetComponent<AIManager>();
        
        enemyAIManager = GetComponent<EnemyAIManager>();


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy)&&!isEnemy)
        {
          
            aiManager.enemiesInRange.Add(other.gameObject);

          
        }

        if (other.TryGetComponent<AIManager>(out AIManager solider) && isEnemy)
        {

            enemyAIManager.enemiesInRange.Add(other.gameObject);


        }




    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy)&&!isEnemy)
        {

            aiManager.enemiesInRange.Remove(other.gameObject);


        }


        if (other.TryGetComponent<AIManager>(out AIManager solider) && isEnemy)
        {

            enemyAIManager.enemiesInRange.Remove(other.gameObject);


        }
    }

}
