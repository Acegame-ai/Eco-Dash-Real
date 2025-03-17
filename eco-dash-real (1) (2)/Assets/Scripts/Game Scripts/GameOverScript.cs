// using UnityEngine;

// public class GameOverScript : MonoBehaviour
// {
//     // Method to detect when the player hits an obstacle (non-trigger collision)
//     private void OnCollisionEnter(Collision collision)
//     {
//         // Check if the collided object is an obstacle
//         if (collision.gameObject.CompareTag("Obstacle"))
//         {
//             Debug.Log("Player hit an obstacle: " + collision.gameObject.name);
//             DisablePhysics();  // Prevent player from flying away
//             TriggerGameOver();
//         }
//         else
//         {
//             Debug.Log("Collided with: " + collision.gameObject.name); // Check what the player is colliding with
//         }
//     }

//     // Method to trigger the Game Over state
//     void TriggerGameOver()
//     {
//         Debug.Log("Game Over!");  // Log for confirmation

//         // Save the score and coin data
//         ScoreManager.Instance.SaveScoreData();  // Corrected from "instance" to "Instance"

//         // Update the GameManager's state to GameOver
//         GameManager.Instance.GameOver();
//     }

//     // Method to disable physics on the player to prevent flying away
//     void DisablePhysics()
//     {
//         Rigidbody rb = GetComponent<Rigidbody>();
//         if (rb != null)
//         {
//             rb.isKinematic = true; // Make Rigidbody kinematic to stop physical interactions
//         }
//     }

//     // Optional method to restart the game if required
//     public void RestartGame()
//     {
//         ScoreManager.Instance.ResetScoreData();  // Corrected from "instance" to "Instance"
//         GameManager.Instance.StartGame();        // Restart the game state
//     }
// }
