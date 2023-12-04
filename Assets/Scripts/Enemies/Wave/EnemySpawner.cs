using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject SpawnEnemy(GameObject enemyPrefab, Transform spawnPoint)
    {
        return Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
