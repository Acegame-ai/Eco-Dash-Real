using UnityEngine;
using System.Collections.Generic;

public class EndlessEnvironmentLoop : MonoBehaviour
{
    public GameObject environmentPrefab; // The prefab for the environment
    public Transform player; // Reference to the player
    public int numberOfPrefabs = 3; // Number of prefabs to keep in the scene
    public float spawnDistance; // The custom distance between each spawn
    private Queue<GameObject> activeEnvironments = new Queue<GameObject>(); // Queue to store active prefabs
    private float lastSpawnZ; // The Z position for the next spawn

    void Start()
    {
        // Automatically find the player by tag if it’s not assigned in the Inspector
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("Player object not found in the scene. Please ensure it has the 'Player' tag.");
                return; // Exit if player is not found to avoid errors in Update
            }
        }

        // Initialize the spawn position based on the custom spawn distance
        lastSpawnZ = player.position.z;

        // Spawn initial environments
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            SpawnEnvironment();
        }
    }

    void Update()
    {
        if (player == null) return; // Safeguard to prevent errors if player reference is missing

        // Check if the player has moved far enough to trigger the next spawn
        if (player.position.z > lastSpawnZ - numberOfPrefabs * spawnDistance)
        {
            SpawnEnvironment();
            RemoveEnvironment();
        }
    }

    void SpawnEnvironment()
    {
        // Instantiate a new environment prefab at the calculated position
        GameObject go = Instantiate(environmentPrefab, Vector3.forward * lastSpawnZ, Quaternion.identity);
        activeEnvironments.Enqueue(go);

        // Update the spawn position for the next environment
        lastSpawnZ += spawnDistance;
    }

    void RemoveEnvironment()
    {
        // Remove the oldest environment prefab when it's out of view
        if (activeEnvironments.Count > 0)
        {
            Destroy(activeEnvironments.Dequeue());
        }
    }
}
