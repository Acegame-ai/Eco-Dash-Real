using UnityEngine;
using TMPro;

public class FinalScoreUI : MonoBehaviour
{
    public TMP_Text finalScoreText; // Reference to the TextMeshProUGUI to display the final score

    void Start()
    {
        // Hide the final score UI initially
        gameObject.SetActive(false);
    }

    // Method to show the final score when the game ends
    public void ShowFinalScore()
    {
        // Get the final score from the Counting script (via GameManager)
        counting _counting = GameManager.Instance.player.GetComponent<counting>();

        if (_counting != null)
        {
            // Retrieve the final score
            int finalScore = _counting.GetCurrentScore();

            // Update the final score UI element
            finalScoreText.text = "Final Score: " + finalScore.ToString();

            // Show the final score UI
            gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Counting script is missing on the player GameObject.");
        }
    }
}
