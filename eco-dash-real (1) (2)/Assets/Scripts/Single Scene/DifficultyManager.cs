using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance;

    [Tooltip("Time (in seconds) between difficulty increases.")]
    public float difficultyIncreaseInterval = 10f;
    
    [Tooltip("How much the speed multiplier increases every interval.")]
    public float multiplierIncrement = 0.1f;

    // Starting multiplier (1 means no change)
    public float speedMultiplier = 1f;

    private float timer = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Uncomment the line below if you want this manager to persist between scenes:
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= difficultyIncreaseInterval)
        {
            timer = 0f;
            speedMultiplier += multiplierIncrement;
            Debug.Log("DifficultyManager: Speed multiplier increased to " + speedMultiplier);
        }
    }
}
