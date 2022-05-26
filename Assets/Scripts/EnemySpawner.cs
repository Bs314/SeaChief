using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    [SerializeField] GameObject enemyCrab;
    [SerializeField] GameObject enemyOctopus;
    [SerializeField] List<GameObject> SpawnPoints;
    [SerializeField] float spawnRate = 5;
    [SerializeField] float spawnRateVar = 1;
    [SerializeField] AudioClip waterSplash;
    [SerializeField] float RushTime = 60f;

    bool octopusTurn = false;
    bool isEnemySpawning = false;
    int enemyCounter;
    GameStage gameStage;

    void Start()
    {
        gameStage = FindObjectOfType<GameStage>();
        StageUpdate();
        enemyCounter = 0;
    }

    private void StageUpdate()
    {
        int stageInfo = gameStage.GetDeathCount();
        if(stageInfo>2)
        {
            octopusTurn = true;
            RushTime += stageInfo*10;
        }
    }

    void Update()
    {
        CheckEnemySpawning();
        RushTime -= Time.deltaTime;
        
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
        if(RushTime<0)spawnSpeed = 1;
        yield return new WaitForSeconds(spawnSpeed);
        int spawnIndex = Random.Range(0,4);
        enemyCounter++;
        if(enemyCounter>5 && octopusTurn)
        {
            enemyCounter = 0;
            Instantiate(enemyOctopus,SpawnPoints[spawnIndex].transform.position,Quaternion.identity);

        }
        else
        {
            Instantiate(enemyCrab,SpawnPoints[spawnIndex].transform.position,Quaternion.identity);    
        }
        
        AudioSource.PlayClipAtPoint(waterSplash,SpawnPoints[spawnIndex].transform.position,0.4f);
        isEnemySpawning = false;
        
    }


    
}
