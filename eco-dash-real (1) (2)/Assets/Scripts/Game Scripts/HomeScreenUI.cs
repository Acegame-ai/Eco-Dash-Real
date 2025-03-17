using UnityEngine;
using TMPro;

public class HomeScreenUI : MonoBehaviour
{
    public TMP_Text totalScoreText;
    public TMP_Text totalCoinsText;

    void Start()
    {
        // Retrieve the score and coin data from PlayerPrefs
        int savedScore = PlayerPrefs.GetInt("FinalScore", 0);
        int savedCoins = PlayerPrefs.GetInt("TotalCoins", 0);

        totalScoreText.text = " " + savedScore.ToString();
        totalCoinsText.text = " " + savedCoins.ToString();
    }
}
