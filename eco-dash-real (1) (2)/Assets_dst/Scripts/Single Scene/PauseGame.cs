using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenuUI; // Assign the pause menu UI in Inspector

    private void OnEnable()
    {
        // Subscribe to the pause event
        EventManager.Instance.onPauseGame.AddListener(ActivatePauseMenu);
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        EventManager.Instance.onPauseGame.RemoveListener(ActivatePauseMenu);
    }

    private void ActivatePauseMenu()
    {
        pauseMenuUI.SetActive(true); // Activate the pause menu UI
    }

    public void Pause()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.onPauseGame.Invoke(); // Invoke the pause event
        }

       
    }
}
