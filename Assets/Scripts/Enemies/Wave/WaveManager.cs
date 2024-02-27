using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public Transform[] spawnPoints; // Array of spawn points
    public EnemyWave[] waves;       // Array of waves
    private int currentWaveIndex = 0;
    private EnemySpawner spawner;
    private List<GameObject> activeEnemies = new List<GameObject>(); // List of active enemies
    private bool isSpawningWave = false;
    public TMP_Text waveText; // Reference to the text component

    public GameObject dragonPrefab; // Reference to the dragon prefab
    private bool dragonSpawned = false; // To ensure the dragon is only spawned once

    void Start()
    {
        spawner = GetComponent<EnemySpawner>(); // Initialize the spawner
    }

    void Update()
    {
        if (currentWaveIndex < waves.Length)
        {
            var currentWave = waves[currentWaveIndex];
            bool shouldStartNextWave = false;

            switch (currentWave.waveStartMode)
            {
                case EnemyWave.WaveStartMode.AfterClearing:
                    if (activeEnemies.Count == 0 && !isSpawningWave)
                    {
                        shouldStartNextWave = true;
                    }
                    break;

                case EnemyWave.WaveStartMode.AfterTime:
                    if (!isSpawningWave && !IsInvoking("StartNextWave"))
                    {
                        Invoke("StartNextWave", currentWave.timeBetweenWaves);
                    }
                    break;
            }

            if (shouldStartNextWave)
            {
                StartNextWave();
            }
        }
        else if (!dragonSpawned && activeEnemies.Count == 0)
        {
            // All waves are completed and no active enemies are left
            SpawnDragon();
            dragonSpawned = true;
        }

        // Update wave information display
        waveText.text = "Wave: " + (currentWaveIndex) + " | Remaining Enemies: " + activeEnemies.Count;
    }

    private void StartNextWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));
            if (waves[currentWaveIndex].waveStartMode == EnemyWave.WaveStartMode.AfterTime)
            {
                CancelInvoke("StartNextWave");
            }
        }
    }

    IEnumerator SpawnWave(EnemyWave wave)
    {
        isSpawningWave = true;
        yield return new WaitForSeconds(wave.startDelay);

        foreach (var group in wave.enemyGroups)
        {
            yield return new WaitForSeconds(group.spawnTime);

            foreach (var enemyType in group.enemies)
            {
                for (int i = 0; i < enemyType.count; i++)
                {
                    Transform spawnPoint = enemyType.spawnPointIndex == -1 ?
                        spawnPoints[Random.Range(0, spawnPoints.Length)] :
                        spawnPoints[Mathf.Clamp(enemyType.spawnPointIndex, 0, spawnPoints.Length - 1)];

                    GameObject enemy = spawner.SpawnEnemy(enemyType.prefab, spawnPoint);
                    enemy.GetComponent<EnemyTemplate>().SetHealth(enemyType.health);
                    activeEnemies.Add(enemy);
                    enemy.GetComponent<BasicEnemy>().OnDeath += () => OnEnemyDeath(enemy);
                }
            }
        }

        currentWaveIndex++;
        isSpawningWave = false;
    }

    private void SpawnDragon()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject dragon = Instantiate(dragonPrefab, spawnPoint.position, spawnPoint.rotation);
        // Set up any additional properties for the dragon if needed
    }

    private void OnEnemyDeath(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }
}
