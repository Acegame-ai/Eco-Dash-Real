using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _player;
    [SerializeField] private FinalScoreUI _finalScoreUI; // Reference to the FinalScoreUI

    private PlayerMovement _pl;
    private bool _isGameOver = false;

    void Start()
    {
        if (_player != null)
        {
            _pl = _player.GetComponent<PlayerMovement>();
        }

        _gameOverUI.SetActive(false);
    }

    void Update()
    {
        if (_pl == null) return;

        if (!_pl.enabled && !_isGameOver)
        {
            _isGameOver = true;

            // Show Game Over UI
            _gameOverUI.SetActive(true);

            // Show final score
            if (_finalScoreUI != null)
            {
                _finalScoreUI.ShowFinalScore(); // Call ShowFinalScore here
            }
        }
    }

    public void PlayAgain(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void ToHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
