using System.Collections;
using UnityEngine;

public class PlayerBoosters : MonoBehaviour
{
    [Header("Solar Sprint Settings")]
    [Tooltip("Duration of the Solar Sprint boost (seconds).")]
    public float sprintDuration = 5f;
    [Tooltip("Additional force applied to the player during Solar Sprint.")]
    public float sprintForce = 10f;
    [Tooltip("Particle effect for Solar Sprint.")]
    public ParticleSystem solarSprintEffect;
    
    [Header("Recyclotron Settings")]
    [Tooltip("Duration of the Recyclotron boost (seconds).")]
    public float recyclotronDuration = 5f;
    [Tooltip("Radius within which trash will be attracted.")]
    public float attractionRadius = 10f;
    [Tooltip("Force applied to attract trash objects.")]
    public float attractionForce = 5f;
    [Tooltip("Particle effect for Recyclotron.")]
    public ParticleSystem recyclotronEffect;

    private Rigidbody rb;
    private bool isSprinting = false;
    private bool isRecyclotronActive = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("PlayerBoosters: No Rigidbody found on the player!");
        }
    }

    /// <summary>
    /// Activates the Solar Sprint booster.
    /// </summary>
    public void ActivateSolarSprint()
    {
        if (!isSprinting)
        {
            StartCoroutine(SolarSprintRoutine());
        }
    }

    private IEnumerator SolarSprintRoutine()
    {
        isSprinting = true;
        
        // Play the solar sprint effect if assigned.
        if (solarSprintEffect != null)
        {
            solarSprintEffect.Play();
        }

        // Apply an immediate forward force for a burst of speed.
        if (rb != null)
        {
            rb.AddForce(Vector3.forward * sprintForce, ForceMode.VelocityChange);
        }

        // Disable the player's collider to make them invincible.
        Collider playerCollider = GetComponent<Collider>();
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }
        
        // Wait for the sprint duration.
        yield return new WaitForSeconds(sprintDuration);
        
        // Re-enable the player's collider.
        if (playerCollider != null)
        {
            playerCollider.enabled = true;
        }
        
        // Stop the particle effect.
        if (solarSprintEffect != null)
        {
            solarSprintEffect.Stop();
        }
        
        isSprinting = false;
    }

    /// <summary>
    /// Activates the Recyclotron booster.
    /// </summary>
    public void ActivateRecyclotron()
    {
        if (!isRecyclotronActive)
        {
            StartCoroutine(RecyclotronRoutine());
        }
    }

    private IEnumerator RecyclotronRoutine()
    {
        isRecyclotronActive = true;
        
        // Play the recyclotron effect if assigned.
        if (recyclotronEffect != null)
        {
            recyclotronEffect.Play();
        }
        
        float elapsed = 0f;
        while (elapsed < recyclotronDuration)
        {
            AttractTrash();
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        if (recyclotronEffect != null)
        {
            recyclotronEffect.Stop();
        }
        
        isRecyclotronActive = false;
    }

    /// <summary>
    /// Attracts nearby objects tagged "Trash" toward the player.
    /// </summary>
    private void AttractTrash()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attractionRadius);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Trash"))
            {
                Rigidbody trashRb = col.GetComponent<Rigidbody>();
                if (trashRb != null)
                {
                    Vector3 direction = (transform.position - col.transform.position).normalized;
                    trashRb.AddForce(direction * attractionForce, ForceMode.Acceleration);
                }
            }
        }
    }
}
