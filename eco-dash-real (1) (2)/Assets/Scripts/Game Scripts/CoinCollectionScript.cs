// using UnityEngine;
// using TMPro;

// public class CoinCollectionScript : MonoBehaviour
// {
//     // Reference to the TextMeshPro UI component for displaying the coin count (optional)
//     public TextMeshProUGUI coinText;

//     // This method detects when the player enters the coin's trigger
//     private void OnTriggerEnter(Collider other)
//     {
//         // Check if the collided object has the "Coin" tag
//         if (other.gameObject.CompareTag("Coin"))
//         {
//             // Increase the coin count using ScoreManager
//             ScoreManager.Instance.AddCoin(); // Use ScoreManager to update the coin count

//             // Optionally update the coinText if needed
//             if (coinText != null)
//             {
//                 coinText.text = ScoreManager.Instance.GetCoinCount().ToString(); // Sync with ScoreManager
//             }

//             // Destroy the coin object to simulate collection
//             Destroy(other.gameObject);

//             Debug.Log("Coin collected! Total coins: " + ScoreManager.Instance.GetCoinCount());  // Logs the updated coin count
//         }
//     }
// }
