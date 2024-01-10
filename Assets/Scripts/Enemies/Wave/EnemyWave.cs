using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public enum WaveStartMode
    {
        AfterTime,
        AfterClearing
    }

    public WaveStartMode waveStartMode = WaveStartMode.AfterClearing;
    public float startDelay; // Verzögerung vor dem Start der Welle
    public float timeBetweenWaves = 10.0f; // Zeit zwischen Wellen, falls AfterTime gewählt wird

    [System.Serializable]
    public struct EnemyGroup
    {
        public EnemyType[] enemies; // Array of different enemy types
        public float spawnTime;     // Time after the wave start when this group should spawn
    }

    [System.Serializable]
    public struct EnemyType
    {
        public GameObject prefab;       // The prefab for the enemy
        public int count;               // The number of this type of enemy in the group
        public int spawnPointIndex;     // Index des Spawnpoints, -1 für zufällige Auswahl
        public int health;              // Lebenspunkte der Gegner
    }

    public EnemyGroup[] enemyGroups;  // Array of enemy groups
}
