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

    int dailyEnemyCount;
    int maxLiveEnemyCount;

    float spawnInterval;


    private void Awake()
    {
        enemyManager = this;

    }
    private void Start()
    {
        poolManager = GyroPoolManager.poolManager;

        baseManager = BaseDefanceManager.baseDefanceManager;

        CalculateTheWave(GameManager.gameManager.day);
    }


    void CalculateTheWave(int day)
    {
        spawnInterval = 2 / (Mathf.Pow(day, 1.2F)) + 5 + Mathf.Sin(day) / 30;

      

        StartCoroutine(SpawnEnemy("EnemyType1", 0));

    }



    IEnumerator SpawnEnemy(string poolName,float _spawnInterval)
    {
        
        yield return new WaitForSeconds(_spawnInterval);

        int randomPoint = Random.Range(0, spawnPoints.Length);

        GameObject newEnemy= poolManager.Pull(0, spawnPoints[randomPoint].position, spawnPoints[randomPoint].rotation);

        enemyAIManagers.Add(newEnemy.GetComponent<EnemyAIManager>());

        StartCoroutine(newEnemy.GetComponent<EnemyAIManager>().SetupSpawn(5, targetPoints[0]));

        StartCoroutine(SpawnEnemy("EnemyType1", spawnInterval));

    }






}
