using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    [System.Serializable]
    public struct EnemyType
    {
        public GameObject prefab; // The prefab for the enemy
        public int count;         // The number of this type of enemy in the wave
    }

    public EnemyType[] enemies;  // Array of different enemy types
    public float spawnInterval;  // Time interval between spawning each enemy
    public float startDelay;     // Delay before the start of the wave
}
