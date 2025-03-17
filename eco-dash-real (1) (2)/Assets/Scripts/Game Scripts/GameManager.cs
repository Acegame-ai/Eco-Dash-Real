// using UnityEngine;
// using TMPro;
// using UnityEngine.SceneManagement; // Required for scene management

// public enum GameState
// {
//     Running,
//     Paused,
//     GameOver
// }

// public class GameManager : MonoBehaviour
// {
//     public static GameManager Instance { get; private set; }

//     public GameState CurrentState { get; private set; }

//     // UI Panels
//     [SerializeField] private GameObject gameplayUI;
//     [SerializeField] private GameObject pauseMenuUI;
//     [SerializeField] private GameObject gameOverUI;

//     // Score and coin UI elements
//     [SerializeField] private TextMeshProUGUI pauseScoreText;
//     [SerializeField] private TextMeshProUGUI pauseCoinText;
//     [SerializeField] private TextMeshProUGUI gameOverScoreText;
//     [SerializeField] private TextMeshProUGUI gameOverCoinText;

//     private int score;
//     private int coins;

//     private void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject); // Keep GameManager persistent across scenes
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

//     private void Start()
//     {
//         UpdateGameState(GameState.Running);
//     }

//     public void UpdateGameState(GameState newState)
//     {
//         CurrentState = newState;

//         // Hide all UI elements initially
//         gameplayUI?.SetActive(false);
//         pauseMenuUI?.SetActive(false);
//         gameOverUI?.SetActive(false);

//         // Activate the UI and set game behavior based on the current state
//         switch (CurrentState)
//         {
//             case GameState.Running:
//                 gameplayUI?.SetActive(true);
//                 Time.timeScale = 1f;
//                 break;
//             case GameState.Paused:
//                 pauseMenuUI?.SetActive(true);
//                 UpdatePauseUI();
//                 Time.timeScale = 0f;
//                 break;
//             case GameState.GameOver:
//                 gameOverUI?.SetActive(true);
//                 UpdateGameOverUI();
//                 Time.timeScale = 0f;
//                 break;
//         }
//     }

//     // Method to update the score and coin display on the Pause UI
//     private void UpdatePauseUI()
//     {
//         if (pauseScoreText != null) pauseScoreText.text = score.ToString();
//         if (pauseCoinText != null) pauseCoinText.text = coins.ToString();
//     }

//     // Method to update the score and coin display on the Game Over UI
//     private void UpdateGameOverUI()
//     {
//         if (gameOverScoreText != null) gameOverScoreText.text = score.ToString();
//         if (gameOverCoinText != null) gameOverCoinText.text = coins.ToString();
//     }

//     // Method to restart the game in the same scene
//     public void RestartGame()
//     {
//         score = 0;
//         coins = 0;
//         UpdateGameState(GameState.Running);

//         // Reload the current scene to reset everything
//         SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
//     }

//     // Methods to change the game state
//     public void StartGame()
//     {
//         UpdateGameState(GameState.Running);
//     }

//     public void PauseGame()
//     {
//         UpdateGameState(GameState.Paused);
//     }

//     public void ResumeGame()
//     {
//         UpdateGameState(GameState.Running);
//     }

//     public void GameOver()
//     {
//         UpdateGameState(GameState.GameOver);
//     }

//     // Methods to update the score and coins
//     public void AddScore(int amount)
//     {
//         score += amount;
//     }

//     public void AddCoins(int amount)
//     {
//         coins += amount;
//     }

//     private void OnApplicationQuit()
//     {
//         // Add any cleanup code here if needed, e.g., saving game data or handling analytics
//     }
// }
