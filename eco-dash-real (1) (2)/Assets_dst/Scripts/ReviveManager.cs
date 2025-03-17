using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ReviveManager : MonoBehaviour
{
    public static ReviveManager Instance;

    [Header("Revive UI Elements")]
    [Tooltip("The panel that contains the revive UI.")]
    public GameObject revivePanel;
    [Tooltip("The panel for the Game Over UI.")]
    public GameObject gameOverUI;
    [Tooltip("An extra GameObject to be active when Game Over is triggered.")]
    public GameObject extraGameOverObject;
    [Tooltip("Text to show the countdown (in seconds).")]
    public TextMeshProUGUI countdownText;
    [Tooltip("Button to revive by spending coins.")]
    public Button reviveWithCoinsButton;
    [Tooltip("Button to cancel the revive and trigger game over.")]
    public Button cancelButton;

    [Header("Revive Settings")]
    [Tooltip("Duration (in seconds) that the revive UI remains active.")]
    public float reviveDuration = 10f;
    [Tooltip("Number of coins required to revive using coins.")]
    public int requiredCoins = 100;

    private Coroutine countdownCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Optionally, persist this object between scenes:
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Displays the revive UI, pauses the game, and starts the countdown.
    /// If the player's current score (from "counting") is higher than the saved high score,
    /// the revive UI is skipped and Game Over is triggered immediately.
    /// </summary>
    public void ShowReviveUI()
    {
        // Retrieve the current score using your "counting" script.
        int currentScore = FindFirstObjectByType<counting>().GetCurrentScore();
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (currentScore > savedHighScore)
        {
            Debug.Log("ReviveManager: New high score achieved (" + currentScore + " > " + savedHighScore + "). Skipping revive UI.");
            CancelRevive();
            return;
        }

        // Pause the game.
        Time.timeScale = 0f;

        // Activate the revive UI panel.
        if (revivePanel != null)
        {
            revivePanel.SetActive(true);
        }
        else
        {
            Debug.LogError("ReviveManager: revivePanel is not assigned!");
        }

        // Deactivate the Game Over UI and extra object.
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
        if (extraGameOverObject != null)
        {
            extraGameOverObject.SetActive(false);
        }

        // Check the player's coin count using the CoinCollection singleton.
        int currentCoins = (CoinCollection.Instance != null) ? CoinCollection.Instance.GetFinalCoins() : 0;
        Debug.Log("ReviveManager: Player has " + currentCoins + " coins.");

        // Enable the coin-based revive button only if the player has enough coins.
        if (reviveWithCoinsButton != null)
        {
            reviveWithCoinsButton.interactable = (currentCoins >= requiredCoins);
            reviveWithCoinsButton.onClick.RemoveAllListeners();
            reviveWithCoinsButton.onClick.AddListener(ReviveUsingCoins);
        }
        else
        {
            Debug.LogError("ReviveManager: reviveWithCoinsButton is not assigned!");
        }

        // Setup the cancel button.
        if (cancelButton != null)
        {
            cancelButton.onClick.RemoveAllListeners();
            cancelButton.onClick.AddListener(CancelRevive);
        }
        else
        {
            Debug.LogError("ReviveManager: cancelButton is not assigned!");
        }

        // Start the countdown coroutine using realtime (so it's not affected by Time.timeScale).
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
        countdownCoroutine = StartCoroutine(ReviveCountdown());
    }

    private IEnumerator ReviveCountdown()
    {
        float timer = reviveDuration;
        while (timer > 0)
        {
            if (countdownText != null)
            {
                countdownText.text = "Continue in: " + Mathf.CeilToInt(timer).ToString() + "s";
            }
            yield return new WaitForSecondsRealtime(1f);
            timer -= 1f;
        }
        // When time runs out, cancel the revive and trigger Game Over.
        CancelRevive();
    }

    /// <summary>
    /// Attempts to revive the player using coins.
    /// </summary>
    public void ReviveUsingCoins()
    {
        int currentCoins = (CoinCollection.Instance != null) ? CoinCollection.Instance.GetFinalCoins() : 0;
        if (currentCoins >= requiredCoins)
        {
            // Optionally, deduct coins here.
            Debug.Log("ReviveManager: Reviving using coins (deducted " + requiredCoins + " coins).");
            CompleteRevive();
        }
        else
        {
            Debug.Log("ReviveManager: Not enough coins for revive.");
        }
    }

    /// <summary>
    /// Cancels the revive attempt, activates the Game Over UI (and extra object), and triggers Game Over.
    /// </summary>
    public void CancelRevive()
    {
        Debug.Log("ReviveManager: Revive canceled. Activating Game Over UI.");
        HideReviveUI();

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        else
        {
            Debug.LogError("ReviveManager: gameOverUI is not assigned!");
        }
        if (extraGameOverObject != null)
        {
            extraGameOverObject.SetActive(true);
        }

        // Ensure the game remains paused.
        Time.timeScale = 0f;
        // Trigger the Game Over sequence.
        GameManager.Instance.GameOver();
    }

    /// <summary>
    /// Completes the revive: stops the countdown, hides the revive UI,
    /// temporarily sets the player's collider to trigger mode for invincibility,
    /// and resumes gameplay.
    /// </summary>
    private void CompleteRevive()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }
        HideReviveUI();
        // Unpause the game.
        Time.timeScale = 1f;
        // Immediately call the game's revive logic.
        GameManager.Instance.RevivePlayer();
    }

    /// <summary>
    /// Temporarily sets the player's collider to trigger mode (invincibility) for the given duration,
    /// then reverts it back to normal.
    /// </summary>
    /// <param name="duration">Duration in seconds for which the collider remains as trigger.</param>

    /// <summary>
    /// Hides the revive UI panel.
    /// </summary>
    private void HideReviveUI()
    {
        if (revivePanel != null)
        {
            revivePanel.SetActive(false);
        }
    }
}
