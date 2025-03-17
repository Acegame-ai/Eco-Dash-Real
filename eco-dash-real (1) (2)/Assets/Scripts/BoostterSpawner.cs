using UnityEngine;
using System.Collections;

public class BoosterSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [Tooltip("List of booster power-up prefabs to spawn (with BoosterPowerup script attached).")]
    public GameObject[] boosterPrefabs;
    [Tooltip("Potential spawn points for boosters.")]
    public Transform[] spawnPoints;
    [Tooltip("Minimum time (in seconds) between spawns.")]
    public float minSpawnInterval = 30f;
    [Tooltip("Maximum time (in seconds) between spawns.")]
    public float maxSpawnInterval = 60f;

    private void Start()
    {
        StartCoroutine(SpawnBoosterRoutine());
    }

    private IEnumerator SpawnBoosterRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);

            if (boosterPrefabs.Length > 0 && spawnPoints.Length > 0)
            {
                // Choose a random spawn point and a random booster prefab.
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject boosterPrefab = boosterPrefabs[Random.Range(0, boosterPrefabs.Length)];
                Instantiate(boosterPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }
}
