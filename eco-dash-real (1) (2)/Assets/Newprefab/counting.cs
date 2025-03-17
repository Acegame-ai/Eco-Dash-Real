using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class counting : MonoBehaviour
{
    public Transform player; // Reference to the player
    public TextMeshProUGUI scoreText; // Displays score during gameplay
    public TextMeshProUGUI finalScoreText; // Displays final score on Game Over UI
    public GameObject gameOverUI; // Reference to Game Over screen UI

    private float currentScore = 0f;
    private Vector3 lastPosition;
    private bool isGameOver = false;

    void Start()
    {
        if (player != null)
        {
            lastPosition = player.position;
        }

        // Ensure Game Over UI is disabled at the start
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.position, lastPosition);
            // Only add the score if the movement is within a threshold (e.g., less than 5 units)
            if(distance < 5f)
            {
                currentScore += distance * 10f;
            }
            else
            {
                Debug.Log("Large jump ignored: " + distance);
            }
            lastPosition = player.position;
        }

        if (scoreText != null)
        {
            scoreText.text = Mathf.FloorToInt(currentScore).ToString();
        }
    }



    public void GameOver()
    {
        isGameOver = true;

        // Show Game Over UI
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);

        }

        // Display final score
        if (finalScoreText != null)
        {
            finalScoreText.text = Mathf.FloorToInt(currentScore).ToString();
        }

        // Update High Score
        FindFirstObjectByType<HighScoreManager>().CheckForHighScore(GetCurrentScore());

        // Optionally disable the score text (if you want only the final score to be shown)
        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(false);
        }
    }
    public void ResetScoreTracking()
    {
        if (player != null)
        {
            lastPosition = player.position;
            Debug.Log("Score tracking reset: lastPosition updated.");
        }
    }


    public int GetCurrentScore()
    {
        return Mathf.FloorToInt(currentScore);
    }
}
