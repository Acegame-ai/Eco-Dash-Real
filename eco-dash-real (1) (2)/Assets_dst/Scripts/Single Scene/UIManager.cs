using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // UI reference
    public GameObject gameplayUI;

    private void OnEnable()
    {
        // Subscribe to game state change event
        EventManager.Instance.onGameStateChange.AddListener(OnGameStateChanged);
    }

    private void OnDisable()
    {
        // Unsubscribe from event
        EventManager.Instance.onGameStateChange.RemoveListener(OnGameStateChanged);
    }

    private void OnGameStateChanged(GameState newState)
    {
        // Hide all UI initially
        gameplayUI.SetActive(false);

        // Handle UI based on game state
        if (newState == GameState.Running)
        {
            gameplayUI.SetActive(true);
            Time.timeScale = 1f; // Resume the game
        }
    }
}
