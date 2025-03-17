using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;  // Pause menu panel
    [SerializeField] private Button pauseButton;      // Pause button (optional)
    [SerializeField] private Button resumeButton;     // Resume button
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private TextMeshProUGUI countdownText; // Countdown UI text

    private bool isPaused = false;

    private void Start()
    {
        pauseMenuUI.SetActive(false); // Ensure pause menu is hidden at start
        countdownText.gameObject.SetActive(false); // Hide countdown at start

        // Add UI button listeners
        if (pauseButton != null) pauseButton.onClick.AddListener(TogglePause);
        if (resumeButton != null) resumeButton.onClick.AddListener(StartResumeCountdown);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
       if (!isPaused) // If not already paused
        {
            isPaused = true;
            Time.timeScale = 0; // Pause the game
            pauseMenuUI.SetActive(true); // Show pause menu
            backgroundMusic?.Pause();
        }
    }

    private void StartResumeCountdown()
    {
        if (isPaused)
        {
            StartCoroutine(ResumeCountdown());
        }
    }

    private IEnumerator ResumeCountdown()
    {
        pauseMenuUI.SetActive(false); // Hide pause menu
        countdownText.gameObject.SetActive(true); // Show countdown

        int countdown = 3;
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString(); // Update UI text
            yield return new WaitForSecondsRealtime(1f); // Wait without affecting time scale
            countdown--; // Decrease countdown
        }

        countdownText.gameObject.SetActive(false); // Hide countdown
        Time.timeScale = 1; // Resume game
        isPaused = false;
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
