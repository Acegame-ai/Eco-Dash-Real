using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject gameOverScreen; // Game Over UI panel
    public GameObject savemeUI;       // Save Me (revive) UI panel

    private void Start() 
    {
        if (gameOverScreen != null)
            gameOverScreen.SetActive(false);
        if (savemeUI != null)
            savemeUI.SetActive(false);
    }
    
    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            // Retrieve the counting script instance using the new API.
            counting scoreManager = UnityEngine.Object.FindFirstObjectByType<counting>();
            if (scoreManager == null)
            {
                Debug.LogError("PlayerCollision: 'counting' script not found!");
                return;
            }
            
            int currentScore = scoreManager.GetCurrentScore();
            int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);

            // Pause the game.
            Time.timeScale = 0f;

            // If the current score is greater than the saved high score,
            // skip the Save Me UI and show the Game Over UI directly.
            if (currentScore > savedHighScore)
            {
                if (gameOverScreen != null)
                    gameOverScreen.SetActive(true);
                scoreManager.GameOver();
            }
            else
            {
                if (savemeUI != null)
                    savemeUI.SetActive(true);
                scoreManager.GameOver();
            }
        }
    }
}
