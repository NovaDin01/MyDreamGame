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
    private int waveNumber;
    private int enemyCount;
    
    private bool isCheckWave = true;
    
    [SerializeField] float waveTime;
    private float currentWaveTime;
        
    private void Start()
    {   
        waveNumber = 1;
        enemyCount = 2;
        currentWaveTime = 0;
    }

    private void Update()
    {
        currentWaveTime += Time.deltaTime;
        WaveManager();
    }

    void WaveManager()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null && isCheckWave)
        {
            isCheckWave = false; // Начинаем новую волну
            if (currentWaveTime >= waveTime)
            {
                waveNumber++;
                enemyCount += 5; // Увеличиваем количество врагов
                Debug.Log($"WaveNumber - {waveNumber}");
                Debug.Log($"enemyCount - {enemyCount}");
                StartCoroutine(TimeBetweenSpawn());
            }
        }
    }

    private IEnumerator TimeBetweenSpawn()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            yield return new WaitForSeconds(Random.Range(1, 3)); // Задержка между спавнами
            enemyTypeIndex = Random.Range(0, enemies.Length);
            spawnPointsIndex = Random.Range(0, spawnPoints.Count);
            Instantiate(enemies[enemyTypeIndex], spawnPoints[spawnPointsIndex].transform.position, Quaternion.identity);
        }

        // После завершения спавна, разрешаем проверку новой волны
        isCheckWave = true;
        currentWaveTime = 0; // Сбрасываем таймер
    }
}