using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    [SerializeField] GameObject enemy;
    [SerializeField] List<GameObject> SpawnPoints;
    [SerializeField] float spawnRate = 5;
    [SerializeField] float spawnRateVar = 1;
    
    bool isEnemySpawning = false;
    GameStage gameStage;

    void Start()
    {
        gameStage = FindObjectOfType<GameStage>();
        StageUpdate();
    }

    private void StageUpdate()
    {
        int stageInfo = gameStage.GetDeathCount();
        if(stageInfo>1)
        {
            spawnRate = spawnRate - stageInfo + 1;
        }
    }

    void Update()
    {
        CheckEnemySpawning();

    }

    private void CheckEnemySpawning()
    {
        if (!isEnemySpawning)
        {
            StartCoroutine(SpawnEnemy());
        }
    }


    IEnumerator SpawnEnemy()
    {
        isEnemySpawning = true;
        float spawnSpeed = Random.Range(spawnRate-spawnRateVar, spawnRate+spawnRateVar);
        yield return new WaitForSeconds(spawnSpeed);
        int spawnIndex = Random.Range(0,4);
        Instantiate(enemy,SpawnPoints[spawnIndex].transform.position,Quaternion.identity);
        isEnemySpawning = false;
        
    }


    
}
