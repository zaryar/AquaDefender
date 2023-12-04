using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Transform[] spawnPoints; // Array of spawn points
    public EnemyWave[] waves;       // Array of waves
    private int currentWaveIndex = 0;
    private EnemySpawner spawner;
    private List<GameObject> activeEnemies = new List<GameObject>(); // List of active enemies
    private bool isSpawningWave = false;
    public string waveInfo = string.Empty;

    // References to text components
    public TMP_Text waveText;

    void Start()
    {
        spawner = GetComponent<EnemySpawner>(); // Initialize the spawner
    }

    void Update()
    {
        // Check if all enemies of the current wave are defeated and not already spawning
        if (activeEnemies.Count == 0 && currentWaveIndex < waves.Length && !isSpawningWave)
        {
            isSpawningWave = true;
            StartNextWave(); // Start the next wave
        }

        // Update wave information display
        waveInfo = "Wave: " + (currentWaveIndex) + " | Remaining Enemies: " + activeEnemies.Count;
        waveText.text = waveInfo;
    }

    private void StartNextWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            StartCoroutine(SpawnWave(waves[currentWaveIndex])); // Begin spawning the next wave
        }
    }

    IEnumerator SpawnWave(EnemyWave wave)
    {
        yield return new WaitForSeconds(wave.startDelay); // Wait for the start delay

        // Spawn each enemy in the wave
        foreach (var enemyType in wave.enemies)
        {
            for (int i = 0; i < enemyType.count; i++)
            {
                Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)]; // Choose a random spawn point
                GameObject enemy = spawner.SpawnEnemy(enemyType.prefab, spawnPoint); // Spawn the enemy
                activeEnemies.Add(enemy); // Add enemy to the list

                // Subscribe to the enemy's death event
                enemy.GetComponent<BasicEnemy>().OnDeath += () => OnEnemyDeath(enemy);

                yield return new WaitForSeconds(wave.spawnInterval); // Wait between spawns
            }
        }

        currentWaveIndex++; // Increment the wave index
        isSpawningWave = false; // Reset spawning boolean
    }

    private void OnEnemyDeath(GameObject enemy)
    {
        activeEnemies.Remove(enemy); // Remove the enemy from the list on death
    }
}
