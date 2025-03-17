using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;    // Displays the current high score on the welcome screen
    public GameObject newHighScoreUI;        // Panel for the congratulatory message
    public TextMeshProUGUI newHighScoreText;   // Text element for the congratulatory message
    public Button tapToContinueButton;       // Button that says "Tap to Continue"

    private int highScore = 0;

    private void Start()
    {
        // Load the saved high score
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreUI();

        // Ensure the congratulatory UI is hidden initially
        if (newHighScoreUI != null)
        {
            newHighScoreUI.SetActive(false);
        }
    }

    /// <summary>
    /// Checks if the current session's score beats the previous high score.
    /// If yes, updates the high score and shows the congratulatory UI with a "Tap to Continue" button.
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

            // Show congratulatory UI with a "Tap to Continue" prompt
            if (newHighScoreUI != null && newHighScoreText != null && tapToContinueButton != null)
            {
                newHighScoreText.text = "Congratulations!\nNew High Score: " + highScore;
                newHighScoreUI.SetActive(true);

                // Remove any previous listeners, then add our tap event
                tapToContinueButton.onClick.RemoveAllListeners();
                tapToContinueButton.onClick.AddListener(HideNewHighScoreUI);
            }
        }
        else
        {
            Debug.Log("HighScoreManager: No new high score. Current High Score remains: " + highScore);
        }

        UpdateHighScoreUI();
    }

    /// <summary>
    /// Hides the congratulatory high score UI.
    /// This is called when the player taps the "Tap to Continue" button.
    /// </summary>
    public void HideNewHighScoreUI()
    {
        if (newHighScoreUI != null)
        {
            newHighScoreUI.SetActive(false);
            Debug.Log("HighScoreManager: Congratulatory UI hidden after tap.");
        }
    }

    /// <summary>
    /// Updates the high score display in the welcome UI.
    /// </summary>
    private void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            highScoreText.text = " " + highScore;
        }
    }
}
