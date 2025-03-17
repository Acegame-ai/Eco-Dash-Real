using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public counting scoreManager; // Reference to the Counting script
    public GameObject gameOverScreen;
    private void Start() 
    {
        gameOverScreen.SetActive(false);
    }
    
    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Time.timeScale = 0f;
            gameOverScreen.SetActive(true);
            scoreManager.GameOver();
        }
    }
}
