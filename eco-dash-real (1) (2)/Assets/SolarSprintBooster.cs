using System.Collections;
using UnityEngine;
using TMPro;

public class SolarSprintBooster : MonoBehaviour
{
    [Header("Solar Sprint Booster Settings")]
    [Tooltip("Duration of the booster effect (in seconds).")]
    public float boostDuration = 5f;
    [Tooltip("Multiplier to apply to the environment's speed multiplier during the boost.")]
    public float boostMultiplier = 1.5f;
    [Tooltip("Particle effect for the boost (e.g., golden flares).")]
    public ParticleSystem boostEffect;
    [Tooltip("UI Text element that displays the current multiplier.")]
    public TextMeshProUGUI multiplierText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("SolarSprintBooster: Player collected booster.");
            if (boostEffect != null)
            {
                boostEffect.Play();
                Debug.Log("SolarSprintBooster: Boost effect started.");
            }
            // Use the new API to find the DifficultyManager.
            DifficultyManager diffManager = Object.FindFirstObjectByType<DifficultyManager>();
            if (diffManager != null)
            {
                Debug.Log("SolarSprintBooster: Found DifficultyManager. Original multiplier: " + diffManager.speedMultiplier);
                StartCoroutine(BoostRoutine(diffManager));
            }
            else
            {
                Debug.LogError("SolarSprintBooster: DifficultyManager not found in scene!");
            }
            // Destroy the booster after pickup.
            Destroy(gameObject);
        }
    }

    private IEnumerator BoostRoutine(DifficultyManager diffManager)
    {
        float originalMultiplier = diffManager.speedMultiplier;
        // Apply boost.
        diffManager.speedMultiplier = originalMultiplier * boostMultiplier;
        Debug.Log("SolarSprintBooster: Boost applied. New multiplier: " + diffManager.speedMultiplier);
        
        // Update the UI text.
        if (multiplierText != null)
        {
            multiplierText.text = "Multiplier: " + diffManager.speedMultiplier.ToString("F2");
            Debug.Log("SolarSprintBooster: Multiplier UI updated to " + multiplierText.text);
        }
        else
        {
            Debug.LogWarning("SolarSprintBooster: multiplierText is not assigned!");
        }
        
        yield return new WaitForSeconds(boostDuration);
        
        // Revert the multiplier.
        diffManager.speedMultiplier = originalMultiplier;
        Debug.Log("SolarSprintBooster: Boost ended. Multiplier reverted to: " + diffManager.speedMultiplier);
        
        // Update the UI text back to the original multiplier.
        if (multiplierText != null)
        {
            multiplierText.text = "Multiplier: " + originalMultiplier.ToString("F2");
            Debug.Log("SolarSprintBooster: Multiplier UI reverted to " + multiplierText.text);
        }
        if (boostEffect != null)
        {
            boostEffect.Stop();
            Debug.Log("SolarSprintBooster: Boost effect stopped.");
        }
    }
}
