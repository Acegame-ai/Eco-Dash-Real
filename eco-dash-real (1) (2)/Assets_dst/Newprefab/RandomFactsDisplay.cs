using UnityEngine;
using TMPro;

public class RandomFactsDisplay : MonoBehaviour
{
    // An array of 10 facts. You can either hard-code them here...
    // public string[] facts = new string[] {
    //     "Fact 1: ...",
    //     "Fact 2: ...",
    //     "Fact 3: ...",
    //     "Fact 4: ...",
    //     "Fact 5: ...",
    //     "Fact 6: ...",
    //     "Fact 7: ...",
    //     "Fact 8: ...",
    //     "Fact 9: ...",
    //     "Fact 10: ..."
    // };

    // ...or assign them via the Inspector.
    public string[] facts;

    // The TextMeshProUGUI component that will display the fact.
    public TextMeshProUGUI factText;

    private void Start()
    {
        // Check if any facts have been assigned.
        if (facts == null || facts.Length == 0)
        {
            Debug.LogWarning("RandomFactsDisplay: No facts provided!");
            return;
        }

        // Ensure the factText reference is set.
        if (factText == null)
        {
            Debug.LogError("RandomFactsDisplay: Fact text UI reference is not assigned!");
            return;
        }

        // Randomly pick an index between 0 and facts.Length - 1.
        int randomIndex = Random.Range(0, facts.Length);

        // Set the UI text to the randomly chosen fact.
        factText.text = facts[randomIndex];

        Debug.Log("RandomFactsDisplay: Displaying fact: " + facts[randomIndex]);
    }
}
