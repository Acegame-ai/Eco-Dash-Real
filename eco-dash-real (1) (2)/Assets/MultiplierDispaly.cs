using UnityEngine;
using TMPro;

public class MultiplierDisplay : MonoBehaviour
{
    [Tooltip("Text element to display the current multiplier.")]
    public TextMeshProUGUI multiplierText;

    private void Update()
    {
        // Check if the DifficultyManager exists.
        if (DifficultyManager.Instance != null && multiplierText != null)
        {
            // Update the UI text with the current multiplier value, formatted to 2 decimal places.
            multiplierText.text = "X " + DifficultyManager.Instance.speedMultiplier.ToString("F2");
        }
    }
}
