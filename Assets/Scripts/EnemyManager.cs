using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager enemyManager;

    public List<EnemyAIManager> enemyAIManagers;

    GyroPoolManager poolManager;

    BaseDefanceManager baseManager;

    [SerializeField]
    Transform[] targetPoints,spawnPoints;

    private void Awake()
    {
        enemyManager = this;

    }
    private void Start()
    {
        poolManager = GyroPoolManager.poolManager;

        baseManager = BaseDefanceManager.baseDefanceManager;

        StartCoroutine(SpawnEnemy("EnemyType1",1));
    }

    IEnumerator SpawnEnemy(string poolName,float spawnInterval)
    {
        
        yield return new WaitForSeconds(spawnInterval);

        int randomPoint = Random.Range(0, spawnPoints.Length);

        GameObject newEnemy= poolManager.Pull(0, spawnPoints[randomPoint].position, spawnPoints[randomPoint].rotation);

        newEnemy.GetComponent<EnemyAIManager>().GotoTarget(targetPoints[0]);

    }






}
