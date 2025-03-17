using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    // Dictionary to store object pools for each tag
    private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    // Prefabs for coins and obstacles
    public GameObject[] coinPrefabs;
    public GameObject[] obstaclePrefabs;

    [Header("Default Pool Sizes")]
    public int initialCoinPoolSize = 100;
    public int initialObstaclePoolSize = 50;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of ObjectPool detected! Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize the pools
        InitializePool("Coin", coinPrefabs, initialCoinPoolSize);
        InitializePool("Obstacle", obstaclePrefabs, initialObstaclePoolSize);
    }

    /// <summary>
    /// Initialize a pool for a specific tag.
    /// </summary>
    public void InitializePool(string tag, GameObject[] prefabs, int initialSize)
    {
        if (poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool for tag '{tag}' already exists! Skipping initialization.");
            return;
        }

        // Create a new queue for this tag
        poolDictionary[tag] = new Queue<GameObject>();

        for (int i = 0; i < initialSize; i++)
        {
            CreateNewObject(tag, prefabs);
        }
    }

    /// <summary>
    /// Retrieve an object from the pool.
    /// If the pool is empty, dynamically expand it.
    /// </summary>
    public GameObject GetObject(string tag)
    {
        if (poolDictionary.ContainsKey(tag))
        {
            // If the pool is empty, expand it
            if (poolDictionary[tag].Count == 0)
            {
                Debug.LogWarning($"Pool for tag '{tag}' is empty. Expanding pool...");
                ExpandPool(tag);
            }

            GameObject obj = poolDictionary[tag].Dequeue();
            obj.SetActive(true);
            return obj;
        }

        Debug.LogError($"Pool with tag '{tag}' does not exist!");
        return null;
    }

    /// <summary>
    /// Return an object to the pool.
    /// </summary>
    public void ReturnObject(string tag, GameObject obj)
    {
        if (poolDictionary.ContainsKey(tag))
        {
            obj.SetActive(false);
            poolDictionary[tag].Enqueue(obj);
        }
        else
        {
            Debug.LogWarning($"No pool exists for tag: {tag}. Destroying object.");
            Destroy(obj);
        }
    }

    /// <summary>
    /// Dynamically expand a pool when it's empty.
    /// </summary>
    private void ExpandPool(string tag)
    {
        if (tag == "Coin")
        {
            CreateNewObject(tag, coinPrefabs);
        }
        else if (tag == "Obstacle")
        {
            CreateNewObject(tag, obstaclePrefabs);
        }
    }

    /// <summary>
    /// Create a new object and add it to the pool.
    /// </summary>
    private void CreateNewObject(string tag, GameObject[] prefabs)
    {
        GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Length)]);
        obj.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
    }
}
