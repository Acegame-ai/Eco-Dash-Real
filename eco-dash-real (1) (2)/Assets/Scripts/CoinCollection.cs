using UnityEngine;
using TMPro;

public class CoinCollection : MonoBehaviour
{
    public static CoinCollection Instance;

    // UI for realtime coin display during gameplay (session coins)
    public TextMeshProUGUI coinText;
    // UI for final coin count (displayed on the Game Over screen)
    public TextMeshProUGUI finalCoinText;
    // UI for total coins accumulated across sessions
    public TextMeshProUGUI totalCoinText;

    // Coins collected in the current session
    private int coinCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("CoinCollection: Instance set successfully.");
            // Optionally persist this object between scenes:
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("CoinCollection: Multiple instances detected! Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Debug.Log("CoinCollection: Script started. Initial coins: " + coinCount);
        UpdateCoinText();  // Update the realtime coin text
        // Update total coin UI from saved data (if assigned)
        UpdateTotalCoinText();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("CoinCollection: Trigger detected with: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Trash"))
        {
            Debug.Log("CoinCollection: Coin collected!");
            coinCount++;
            UpdateCoinText();
            Destroy(other.gameObject);
        }
    }

    // Updates the realtime coin count UI (session coins)
    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = coinCount.ToString();
            Debug.Log("CoinCollection: coinText updated to: " + coinText.text);
        }
        else
        {
            Debug.LogError("CoinCollection: coinText reference is missing!");
        }
    }

    // Updates the total coin UI based on saved data
    private void UpdateTotalCoinText()
    {
        if (totalCoinText != null)
        {
            int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
            totalCoinText.text = totalCoins.ToString();
            Debug.Log("CoinCollection: totalCoinText updated to: " + totalCoinText.text);
        }
        else
        {
            Debug.LogError("CoinCollection: totalCoinText reference is missing!");
        }
    }

    // Called when the player collides with an obstacle (Game Over)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Update final coin UI with session coin count
            if (finalCoinText != null)
            {
                finalCoinText.text = coinCount.ToString();
                Debug.Log("CoinCollection: finalCoinText updated to: " + finalCoinText.text);
            }
            else
            {
                Debug.LogError("CoinCollection: finalCoinText reference is missing!");
            }

            // Add session coins to the saved total and update total coin UI
            int savedTotal = PlayerPrefs.GetInt("TotalCoins", 0);
            int newTotal = savedTotal + coinCount;
            PlayerPrefs.SetInt("TotalCoins", newTotal);
            PlayerPrefs.Save();
            Debug.Log("CoinCollection: New total coins: " + newTotal);
            UpdateTotalCoinText();
        }
    }

    // Optionally reset the session coin count when restarting the game
    public void ResetCoins()
    {
        Debug.Log("CoinCollection: ResetCoins() called. Resetting coins to 0.");
        coinCount = 0;
        UpdateCoinText();
    }

    // Allows other scripts to access the final coin count if needed
    public int GetFinalCoins()
    {
        return coinCount;
    }
}
