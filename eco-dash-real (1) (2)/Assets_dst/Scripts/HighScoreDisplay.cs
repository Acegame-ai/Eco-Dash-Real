using UnityEngine;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    private void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (highScoreText != null)
        {
            highScoreText.text = "" + highScore;
        }
        else
        {
            Debug.LogError("HighScoreDisplay: highScoreText reference is missing!");
        }
        Debug.Log("" + highScore);
    }
}
