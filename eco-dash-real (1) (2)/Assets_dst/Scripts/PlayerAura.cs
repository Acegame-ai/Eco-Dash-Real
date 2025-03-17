using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerAura : MonoBehaviour
{
    [Tooltip("Particle system representing the aura effect.")]
    public ParticleSystem auraEffect;

    /// <summary>
    /// Activates the aura effect for the specified duration.
    /// </summary>
    /// <param name="duration">Duration in seconds the aura stays active.</param>
    public void ActivateAura(float duration)
    {
        if (auraEffect != null)
        {
            auraEffect.Play();
            StartCoroutine(AuraDuration(duration));
        }
        else
        {
            Debug.LogWarning("PlayerAura: No auraEffect assigned!");
        }
    }

    private IEnumerator AuraDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (auraEffect != null)
        {
            auraEffect.Stop();
        }
    }
}
