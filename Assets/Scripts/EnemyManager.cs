using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager enemyManager;

    public List<EnemyAIManager> enemyAIManagers;

    GyroPoolManager poolManager;

    [SerializeField]
    Transform[] targetPoints,spawnPoints;

    private void Awake()
    {
        enemyManager = this;

    }
    private void Start()
    {
        poolManager = GyroPoolManager.poolManager;

     //   StartCoroutine("EnemyType1", 1);
    }

    IEnumerator SpawnEnemy(string poolName,float spawnInterval)
    {
        
        yield return new WaitForSeconds(spawnInterval);

        int randomPoint = Random.Range(0, spawnPoints.Length);

        enemyAIManagers.Add(poolManager.Pull(poolName, spawnPoints[randomPoint].position, spawnPoints[randomPoint].rotation).GetComponent<EnemyAIManager>());

    }






}
