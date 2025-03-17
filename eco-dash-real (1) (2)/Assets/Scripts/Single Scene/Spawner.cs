using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] obstacles;      // Array of obstacle prefabs
    public GameObject[] coins;          // Array of coin prefabs
    public float spawnInterval = 2f;    // Time between spawns

    [Header("Spawn Points")]
    public Transform[] spawnPoints;     // Array of spawn points for spawning objects

    private float spawnTimer = 0f;

    private void Update()
    {
        // Ensure the game is running before spawning objects
        if (GameManager.Instance != null && GameManager.Instance.currentState == GameState.Running)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInterval)
            {
                SpawnObjects();
                spawnTimer = 0f;
            }
        }
    }

    private void SpawnObjects()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return;
        }

        // Randomly select a spawn point
        Transform selectedSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Randomly decide to spawn an obstacle or a coin
        bool spawnObstacle = Random.value > 0.5f;

        if (spawnObstacle && obstacles.Length > 0)
        {
            // Spawn a random obstacle and set its parent to null
            GameObject obstacle = obstacles[Random.Range(0, obstacles.Length)];
            Instantiate(obstacle, selectedSpawnPoint.position, Quaternion.identity, null);
        }
        else if (coins.Length > 0)
        {
            // Spawn a random coin and set its parent to null
            GameObject coin = coins[Random.Range(0, coins.Length)];
            Instantiate(coin, selectedSpawnPoint.position, Quaternion.identity, null);
        }
    }
}
