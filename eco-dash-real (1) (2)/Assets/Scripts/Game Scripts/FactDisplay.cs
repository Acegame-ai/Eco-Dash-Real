using UnityEngine;
using TMPro;

public class FactDisplay : MonoBehaviour
{
    public ClimateFacts climateFacts; // Reference to the ScriptableObject containing facts
    public TextMeshProUGUI[] factTexts; // Array of TextMeshProUGUI elements

    private void Start()
    {
        ShowRandomFacts();
    }

    public void ShowRandomFacts()
    {
        if (climateFacts != null && climateFacts.facts.Count > 0)
        {
            // Iterate through each TextMeshPro element and assign a random fact
            foreach (var factText in factTexts)
            {
                if (factText != null)
                {
                    // Get a random fact for each TextMeshProUGUI
                    string randomFact = climateFacts.facts[Random.Range(0, climateFacts.facts.Count)];
                    factText.text = randomFact;
                }
            }
        }
        else
        {
            Debug.LogWarning("No facts available in ClimateFacts!");
        }
    }
}
