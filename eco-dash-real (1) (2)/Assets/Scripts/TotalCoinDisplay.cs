using UnityEngine;
using TMPro;

public class TotalCoinDisplay : MonoBehaviour
{
    public TextMeshProUGUI totalCoinText;

    private void Start()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        if (totalCoinText != null)
        {
            totalCoinText.text = totalCoins.ToString();
        }
        else
        {
            Debug.LogError("TotalCoinDisplay: totalCoinText reference is missing!");
        }
        Debug.Log("Total Coins: " + totalCoins);
    }
}
