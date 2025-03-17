using UnityEngine;

public class EnvironmentMover : MonoBehaviour
{
    [Header("Environment Settings")]
    public float moveSpeed = 5f;             // Base speed at which the environment moves
    public Transform player;                 // Reference to the player's transform
    public float resetOffsetZ = -10f;        // Offset behind the player for resetting the tile
    public float environmentLength = 50f;    // Length of the environment object

    [Header("Spawner Settings")]
    public GameObject[] obstacles;           // Array of obstacle prefabs
    public GameObject[] coins;               // Array of coin prefabs
    public float spawnInterval = 1f;         // Interval between spawns
    public Transform[] spawnPoints;          // Array of lane positions for spawning
    public float destroyDistance = 15f;      // Distance behind the player to destroy obstacles/coins

    private float spawnTimer = 0f;           // Timer for spawn intervals
    private Vector3 initialPosition;         // Initial position of the environment

    private void Start()
    {
        // Store the initial position of the environment
        initialPosition = transform.position;

        // Add listener for the game restart event
        EventManager.Instance.onGameRestart.AddListener(ResetEnvironment);
    }

    private void OnDestroy()
    {
        // Remove listener when the object is destroyed to prevent memory leaks
        EventManager.Instance.onGameRestart.RemoveListener(ResetEnvironment);
    }

    private void Update()
    {
        // Only move the environment if the game is running
        if (GameManager.Instance.currentState == GameState.Running)
        {
            if (player == null)
            {
                Debug.LogWarning("EnvironmentMover: Player reference is missing!");
                return;
            }

            // Calculate the effective speed using the DifficultyManager's multiplier (if it exists)
            float effectiveSpeed = moveSpeed;
            if (DifficultyManager.Instance != null)
            {
                effectiveSpeed *= DifficultyManager.Instance.speedMultiplier;
            }

            // Move the environment backward
            transform.Translate(Vector3.back * effectiveSpeed * Time.deltaTime);

            // Reset the environment position if the player has passed the full length
            if (player.position.z - transform.position.z >= environmentLength)
            {
                ResetPosition();
            }

            // Handle obstacle and coin spawning
            HandleSpawning();
        }
    }

    private void ResetPosition()
    {
        // Your existing repositioning code, e.g.:
        transform.position += Vector3.forward * environmentLength;
        
        // Reset the score tracking so the jump isn't counted:
        FindFirstObjectByType<counting>().ResetScoreTracking();
    }


    private void ResetEnvironment()
    {
        // Reset the environment to its initial position
        transform.position = initialPosition;
        Debug.Log("EnvironmentMover: Environment reset to initial position.");
    }

    private void HandleSpawning()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnObstaclesAndCoins();
            spawnTimer = 0f;
        }
    }

    private void SpawnObstaclesAndCoins()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("EnvironmentMover: No spawn points assigned!");
            return;
        }

        // Randomly select one lane to leave free
        int openLaneIndex = Random.Range(0, spawnPoints.Length);

        // Spawn obstacles in lanes other than the open one
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (i != openLaneIndex && obstacles.Length > 0)
            {
                // 50% chance to spawn an obstacle in this lane
                bool spawnObstacle = Random.Range(0, 2) == 0;
                if (spawnObstacle)
                {
                    // Randomly decide to spawn 1 or 2 obstacles in sequence
                    int obstacleSpaces = Random.Range(1, 3);
                    for (int j = 0; j < obstacleSpaces; j++)
                    {
                        Vector3 spawnPosition = spawnPoints[i].position + Vector3.forward * j * 2f;
                        GameObject obstaclePrefab = obstacles[Random.Range(0, obstacles.Length)];
                        GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
                        AddMovementToObject(obstacle);
                    }
                }
            }
        }

        // Spawn coins in the open lane
        if (coins.Length > 0)
        {
            int coinCount = Random.Range(6, 12); // Randomly 2 or 3 coins
            for (int i = 0; i < coinCount; i++)
            {
                Vector3 spawnPosition = spawnPoints[openLaneIndex].position + Vector3.forward * i * 2f;
                GameObject coinPrefab = coins[Random.Range(0, coins.Length)];
                GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
                AddMovementToObject(coin);
            }
        }
    }

    private void AddMovementToObject(GameObject obj)
    {
        // Attach an ObstacleMover script if not already present
        ObstacleMover obstacleMover = obj.GetComponent<ObstacleMover>();
        if (obstacleMover == null)
        {
            obstacleMover = obj.AddComponent<ObstacleMover>();
        }

        // Calculate the effective speed for spawned objects
        float effectiveSpeed = moveSpeed;
        if (DifficultyManager.Instance != null)
        {
            effectiveSpeed *= DifficultyManager.Instance.speedMultiplier;
        }
        obstacleMover.Initialize(effectiveSpeed, player, destroyDistance);
    }
}
 