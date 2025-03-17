using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    [Header("Score Settings")]
    public Transform player;              // Reference to the player's transform
    public TextMeshProUGUI scoreText;     // UI element for the score
    public float scoreMultiplier = 0.5f;  // Multiplier to control the score speed
    [Tooltip("Maximum acceptable change in Z position per frame. Values beyond this are ignored.")]
    public float maxDeltaThreshold = 10f; // Threshold to ignore abnormal jumps

    private float score = 0f;             // Player's score
    private float lastPlayerZ = 0f;       // Last frame's Z-position

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("PlayerScore: Player reference is missing!");
            return;
        }
        // Initialize the last known Z position and score.
        lastPlayerZ = player.position.z;
        score = 0f;
        UpdateScoreText();
    }

    private void Update()
    {
        // Only update score if the game is running.
        if (GameManager.Instance.currentState == GameState.Running)
        {
            float currentZ = player.position.z;
            float delta = currentZ - lastPlayerZ;

            // Only add to the score if the delta is positive (moving forward) 
            // and less than the threshold (to avoid jumps when the environment loops).
            if (delta > 0f && delta < maxDeltaThreshold)
            {
                score += delta * scoreMultiplier;
            }

            lastPlayerZ = currentZ;
            UpdateScoreText();
        }
    }

    // Updates the score UI text.
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = Mathf.FloorToInt(score).ToString();
        }
        else
        {
            Debug.LogError("PlayerScore: scoreText reference is missing!");
        }
    }

    // Resets the score (and resets the lastPlayerZ to the current player's Z).
    public void ResetScore()
    {
        score = 0f;
        if (player != null)
        {
            lastPlayerZ = player.position.z;
        }
        UpdateScoreText();
    }
}
