using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;    // Displays the current high score on the welcome screen
    public GameObject newHighScoreUI;        // Panel for the congratulatory message
    public TextMeshProUGUI newHighScoreText;   // Text element for the congratulatory message
    public Button tapToContinueButton;       // Button that says "Tap to Continue"

    public GameObject gameOverUI;            // The Game Over UI panel

    private int highScore = 0;

    private void Start()
    {
        // Hide the Game Over UI at the start.
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        // Load the saved high score.
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreUI();

        // Hide the congratulatory UI initially.
        if (newHighScoreUI != null)
        {
            newHighScoreUI.SetActive(false);
        }
    }

    /// <summary>
    /// Checks if the current session's final score beats the previous high score.
    /// If yes, updates the high score and shows the congratulatory UI.
    /// </summary>
    /// <param name="finalScore">The score from the current session.</param>
    public void CheckForHighScore(int finalScore)
    {
        Debug.Log("HighScoreManager: Checking high score. Final Score = " + finalScore);
        if (finalScore > highScore)
        {
            highScore = finalScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            Debug.Log("HighScoreManager: New High Score Saved: " + highScore);

            // Show congratulatory UI with a "Tap to Continue" prompt.
            if (newHighScoreUI != null && newHighScoreText != null && tapToContinueButton != null)
            {
                newHighScoreText.text = "Congratulations!\nNew High Score: " + highScore;
                newHighScoreUI.SetActive(true);

                // Remove previous listeners and add the tap event.
                tapToContinueButton.onClick.RemoveAllListeners();
                tapToContinueButton.onClick.AddListener(HideNewHighScoreUI);
            }
            else
            {
                Debug.LogError("HighScoreManager: One or more UI references (newHighScoreUI, newHighScoreText, tapToContinueButton) are missing!");
            }
        }
        else
        {
            Debug.Log("HighScoreManager: No new high score. Current High Score remains: " + highScore);
        }

        UpdateHighScoreUI();
    }

    /// <summary>
    /// Hides the congratulatory high score UI and activates the Game Over UI.
    /// This is called when the player taps the "Tap to Continue" button.
    /// </summary>
    public void HideNewHighScoreUI()
    {
        if (newHighScoreUI != null)
        {
            newHighScoreUI.SetActive(false);
            Debug.Log("HighScoreManager: Congratulatory UI hidden after tap.");
             if (gameOverUI != null)
            {
                gameOverUI.SetActive(true);
            }
            else
            {
                Debug.LogWarning("HighScoreManager: gameOverUI reference is missing!");
            }
        }
        else
        {
            Debug.LogWarning("HighScoreManager: newHighScoreUI reference is missing!");
        }

       
    }

    /// <summary>
    /// Updates the high score display.
    /// </summary>
    private void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
        }
        else
        {
            Debug.LogError("HighScoreManager: highScoreText reference is missing!");
        }
    }
}
