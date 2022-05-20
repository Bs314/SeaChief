using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    [SerializeField] GameObject enemy;
    [SerializeField] List<GameObject> SpawnPoints;
    [SerializeField] float spawnRate = 5;
    

    void Start()
    {
      
    }

    void Update()
    {
        SpawnEnemy();      
    }

    void SpawnEnemy()
    {
        
        StartCoroutine(ThrowEnemy());
        
    }

    IEnumerator ThrowEnemy()
    {
        yield return new WaitForSeconds(spawnRate);
        int spawnIndex = Random.Range(0,4);
        Instantiate(enemy,SpawnPoints[spawnIndex].transform.position,Quaternion.identity);
        
    }


    
}
