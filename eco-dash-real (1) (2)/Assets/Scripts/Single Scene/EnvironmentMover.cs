using UnityEngine;
using System.Collections;

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
    public Transform[] spawnPoints;          // Array of lane positions for spawning (e.g., 3 lanes)
    public float spawnInterval = 1f;         // Interval between obstacle/coin spawns
    public float destroyDistance = 15f;      // Distance behind the player to destroy obstacles/coins

    [Header("Booster Settings")]
    [Tooltip("Array of booster power-up prefabs.")]
    public GameObject[] boosters;
    [Tooltip("Time interval (in seconds) between booster spawn attempts.")]
    public float boosterSpawnInterval = 30f;
    [Tooltip("Chance (0 to 1) to spawn a booster at each spawn interval.")]
    public float boosterSpawnChance = 0.5f;

    private float spawnTimer = 0f;           // Timer for obstacle/coin spawns
    private float boosterSpawnTimer = 0f;    // Timer for booster spawns
    private Vector3 initialPosition;         // Initial position of the environment

    // Store the open lane index determined during obstacle/coin spawning.
    private int lastOpenLaneIndex = 0;

    private void Start()
    {
        // Store the initial position of the environment.
        initialPosition = transform.position;

        // Add listener for the game restart event.
        EventManager.Instance.onGameRestart.AddListener(ResetEnvironment);
    }

    private void OnDestroy()
    {
        // Remove listener when the object is destroyed to prevent memory leaks.
        EventManager.Instance.onGameRestart.RemoveListener(ResetEnvironment);
    }

    private void Update()
    {
        // Only move the environment if the game is running.
        if (GameManager.Instance.currentState == GameState.Running)
        {
            if (player == null)
            {
                Debug.LogWarning("EnvironmentMover: Player reference is missing!");
                return;
            }

            // Calculate the effective speed using the DifficultyManager's multiplier (if it exists).
            float effectiveSpeed = moveSpeed;
            if (DifficultyManager.Instance != null)
            {
                effectiveSpeed *= DifficultyManager.Instance.speedMultiplier;
            }

            // Move the environment backward.
            transform.Translate(Vector3.back * effectiveSpeed * Time.deltaTime);

            // Reset the environment position if the player has passed the full length.
            if (player.position.z - transform.position.z >= environmentLength)
            {
                ResetPosition();
            }

            // Handle obstacle and coin spawning.
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                SpawnObstaclesAndCoins();
                spawnTimer = 0f;
            }

            // Handle booster spawning.
            boosterSpawnTimer += Time.deltaTime;
            if (boosterSpawnTimer >= boosterSpawnInterval)
            {
                if (Random.value < boosterSpawnChance)
                {
                    SpawnBooster();
                }
                boosterSpawnTimer = 0f;
            }
        }
    }

    private void ResetPosition()
    {
        // Move the environment forward by its total length.
        transform.position += Vector3.forward * environmentLength;
        
        // Reset the score tracking so the jump isn't counted.
        FindFirstObjectByType<counting>().ResetScoreTracking();
    }

    private void ResetEnvironment()
    {
        // Reset the environment to its initial position.
        transform.position = initialPosition;
        Debug.Log("EnvironmentMover: Environment reset to initial position.");
    }

    private void SpawnObstaclesAndCoins()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("EnvironmentMover: No spawn points assigned!");
            return;
        }

        // Randomly select one lane to leave free and store it.
        lastOpenLaneIndex = Random.Range(0, spawnPoints.Length);

        // Spawn obstacles in lanes other than the open one.
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (i != lastOpenLaneIndex && obstacles.Length > 0)
            {
                // 50% chance to spawn an obstacle in this lane.
                bool spawnObstacle = Random.Range(0, 2) == 0;
                if (spawnObstacle)
                {
                    int obstacleSpaces = Random.Range(1, 3); // Spawn 1 or 2 obstacles in sequence.
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

        // Spawn coins in the open lane.
        if (coins.Length > 0)
        {
            int coinCount = Random.Range(6, 12);
            for (int i = 0; i < coinCount; i++)
            {
                Vector3 spawnPosition = spawnPoints[lastOpenLaneIndex].position + Vector3.forward * i * 2f;
                GameObject coinPrefab = coins[Random.Range(0, coins.Length)];
                GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
                AddMovementToObject(coin);
            }
        }
    }

    private void SpawnBooster()
    {
        // Use the last open lane (the lane that was free for coins).
        if (boosters.Length > 0 && spawnPoints.Length > 0)
        {
            Transform spawnPoint = spawnPoints[lastOpenLaneIndex];
            GameObject boosterPrefab = boosters[Random.Range(0, boosters.Length)];
            GameObject booster = Instantiate(boosterPrefab, spawnPoint.position, spawnPoint.rotation);
            AddMovementToObject(booster);
            Debug.Log("EnvironmentMover: Booster spawned at lane " + lastOpenLaneIndex);
        }
    }

    private void AddMovementToObject(GameObject obj)
    {
        // Instead of adding an ObstacleMover, simply set the object as a child.
        obj.transform.SetParent(transform);
        
        // Optionally, if you want to destroy objects that are too far behind,
        // you can add a simple component (or check their localPosition) in Update().
    }

}
