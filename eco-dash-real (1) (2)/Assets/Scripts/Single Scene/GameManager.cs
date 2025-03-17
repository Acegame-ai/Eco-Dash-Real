using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;
    public GameObject player; // Reference to the player GameObject

    // Cinemachine Cameras
    public CinemachineVirtualCamera homeCamera;
    public CinemachineVirtualCamera runningCamera;

    public float moveSpeed = 5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateGameState(GameState.Running);
    }

    public void UpdateGameState(GameState newState)
    {
        if (currentState == newState) return;

        currentState = newState;
        EnterState(newState);
    }

    private void EnterState(GameState state)
    {
        if (state == GameState.Running)
        {
            Debug.Log("GameManager: Entering Running State...");
            Time.timeScale = 1f; // Resume game logic
            runningCamera.Priority = 10;
            homeCamera.Priority = 5;
        }
        else if (state == GameState.GameOver)
        {
            Debug.Log("GameManager: Entering GameOver State...");
            Time.timeScale = 0f; // Pause game logic
            // You can adjust cameras or show a Game Over UI here.
        }
    }

    /// <summary>
    /// Call this method to trigger game over.
    /// </summary>
    public void GameOver()
    {
        Debug.Log("GameManager: Game Over triggered.");
        UpdateGameState(GameState.GameOver);
        // Additional Game Over logic (e.g., showing UI) goes here.
    }
    

    /// <summary>
    /// Call this method to revive the player.
    /// </summary>
    public void RevivePlayer()
    {
        Debug.Log("GameManager: Reviving player.");
        // Implement additional revive logic if needed, such as repositioning the player.
        UpdateGameState(GameState.Running);
    }
}
