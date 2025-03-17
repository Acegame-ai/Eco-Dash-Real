// using UnityEngine;
// using TMPro;

// public class ScoreManager : MonoBehaviour
// {
//     public static ScoreManager Instance; // Singleton instance

//     // Main game score and coin texts
//     public TMP_Text scoreText;
//     public TMP_Text coinText;

//     // Pause UI score and coin texts
//     public TMP_Text pauseScoreText;
//     public TMP_Text pauseCoinText;

//     // Game Over UI score and coin texts
//     public TMP_Text gameOverScoreText;
//     public TMP_Text gameOverCoinText;

//     private int score = 0;
//     private int coins = 0;
//     public float scoreIncreaseInterval = 1f;
//     private float elapsedTime = 0f;

//     void Awake()
//     {
//         // Singleton pattern to maintain one instance
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

//     void Start()
//     {
//         UpdateAllUI(); // Initialize the UI
//     }

//     void Update()
//     {
//         // Automatically increase the score over time
//         elapsedTime += Time.deltaTime;
//         if (elapsedTime >= scoreIncreaseInterval)
//         {
//             AddScore(1); // Increase score by 1
//             elapsedTime = 0f; // Reset the timer
//         }
//     }

//     public void AddCoin()
//     {
//         coins++;
//         UpdateAllUI();
//     }

//     public void AddScore(int amount)
//     {
//         score += amount;
//         UpdateAllUI();
//     }

//     public int GetCoinCount()
//     {
//         return coins;
//     }

//     public int GetScore()
//     {
//         return score;
//     }

//     // Update all UI elements
//     void UpdateAllUI()
//     {
//         if (scoreText != null) scoreText.text = score.ToString();
//         if (coinText != null) coinText.text = coins.ToString();
//         if (pauseScoreText != null) pauseScoreText.text = score.ToString();
//         if (pauseCoinText != null) pauseCoinText.text = coins.ToString();
//         if (gameOverScoreText != null) gameOverScoreText.text = score.ToString();
//         if (gameOverCoinText != null) gameOverCoinText.text = coins.ToString();
//     }

//     // Called when the game is paused
//     public void OnPause()
//     {
//         UpdateAllUI();
//     }

//     // Called when the game is over
//     public void OnGameOver()
//     {
//         UpdateAllUI();           // Update the Game Over UI
//         Time.timeScale = 0;      // Stop the game
//         SaveScoreData();         // Save the score and coin data
//     }

//     public void SaveScoreData()
//     {
//         PlayerPrefs.SetInt("FinalScore", score);
//         PlayerPrefs.SetInt("TotalCoins", coins);
//         PlayerPrefs.Save();
//     }

//     public void ResetScoreData()
//     {
//         score = 0;
//         coins = 0;
//         UpdateAllUI();
//     }

//     // Method to restart or resume the game
//     public void RestartGame()
//     {
//         Time.timeScale = 1; // Resume the game
//         ResetScoreData();   // Reset the scores and coins
//         // Logic to reset the game state
//         // Example: Reload the active scene
//         UnityEngine.SceneManagement.SceneManager.LoadScene(
//             UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
//     }
// }
