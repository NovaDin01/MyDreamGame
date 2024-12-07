using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWaveManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnPoints;
    [SerializeField] private GameObject[] enemies;
    private int enemyTypeIndex;
    private int spawnPointsIndex;
    private int waweNumber;

    private void Update()
    {
        SpawnEnemy();
    }

    void Wave()
    {
        
    }

    void SpawnEnemy()
    {
        StartCoroutine(TimeBetweenSpawn());
    }

    private IEnumerator TimeBetweenSpawn()
    {
        for (int i = 0; i < waweNumber; i++)
        {
            yield return new WaitForSeconds(Random.Range(2, 5));  // Задержка
            enemyTypeIndex = Random.Range(0, enemies.Length);
            spawnPointsIndex = Random.Range(0, spawnPoints.Count);
            Instantiate(enemies[enemyTypeIndex], spawnPoints[spawnPointsIndex].transform.position, Quaternion.identity);
        }
    } 
}