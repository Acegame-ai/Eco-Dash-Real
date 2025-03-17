using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScore : MonoBehaviour
{
    public TextMeshProUGUI _highscoreText;
    private float _highScore;

    void Start()
    {
        _highScore = PlayerPrefs.GetFloat("HighScore", 0);
        _highscoreText.text = _highScore.ToString("0");
    }

    public void CheckForHighScore(float _currentScore)
    {
        if (_currentScore > _highScore)
        {
            _highScore = _currentScore;
            _highscoreText.text = _highScore.ToString("0");

            PlayerPrefs.SetFloat("HighScore", _highScore);
        }
    }
}

