// using UnityEngine;
// using UnityEngine.UI;

// public class PauseGame : MonoBehaviour
// {
//     public GameObject pauseMenuUI;  // Reference to the Pause Menu UI panel
//     public GameObject gameOverUI;   // Reference to the Game Over UI panel
//     public Button pauseButton;      // Reference to the Pause button
//     public Button resumeButton;     // Reference to the Resume button within the pause menu
//     public Button quitButton;       // Reference to the Quit button within the pause menu

//     void Start()
//     {
//         // Ensure the game starts in an unpaused state
//         GameManager.Instance.ResumeGame();  // Start the game in the Running state
//         pauseMenuUI.SetActive(false);       // Hide the pause menu initially
//         gameOverUI.SetActive(false);        // Hide the game over menu initially
//     }

//     // Called when the player clicks the Pause button
//     public void Pause()
//     {
//         GameManager.Instance.PauseGame();          // Update the game state to Paused
//         Time.timeScale = 0;                        // Freeze the game time
//         pauseMenuUI.SetActive(true);               // Show the pause menu
//         pauseButton.gameObject.SetActive(false);   // Hide the Pause button
//         ScoreManager.Instance.OnPause();           // Update the Pause UI with current score and coins
//     }

//     // Called when the player clicks the Resume button
//     public void Resume()
//     {
//         GameManager.Instance.ResumeGame();         // Update the game state to Running
//         Time.timeScale = 1;                        // Resume the game time
//         pauseMenuUI.SetActive(false);              // Hide the pause menu
//         pauseButton.gameObject.SetActive(true);    // Show the Pause button
//     }

//     // Called when the game over event is triggered
//     public void TriggerGameOver()
//     {
//         GameManager.Instance.GameOver();           // Update the game state to GameOver
//         Time.timeScale = 1;                        // Make sure the game time is running
//         gameOverUI.SetActive(true);                // Show the game over menu
//         ScoreManager.Instance.OnGameOver();        // Update the Game Over UI with final score and coins
//     }

//     // Called when the player clicks the Quit button
//     public void QuitGame()
//     {
//         GameManager.Instance.ResumeGame(); // Make sure the game is unpaused before quitting

//         // Load the main menu or quit the application
//         #if UNITY_EDITOR
//             UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
//         #else
//             Application.Quit(); // Quit the application if in a build
//         #endif
//     }
// }
