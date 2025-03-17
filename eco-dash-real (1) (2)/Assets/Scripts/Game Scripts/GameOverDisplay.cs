using UnityEngine;
using TMPro;

public class GameOverDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinsText;

    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0); // Matches ScoreManager key
        int finalCoins = PlayerPrefs.GetInt("TotalCoins", 0);  // Matches ScoreManager key

        scoreText.text = " " + finalScore;
        coinsText.text = " " + finalCoins;
    }
}
