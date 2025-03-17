using System.Collections;
using UnityEngine;

public class RecyclotronBooster : MonoBehaviour
{
    [Header("Recyclotron Booster Settings")]
    [Tooltip("Duration of the booster effect (in seconds).")]
    public float boostDuration = 5f;
    [Tooltip("Radius within which trash will be attracted.")]
    public float magnetRange = 10f;
    [Tooltip("Strength of the magnet force.")]
    public float attractionForce = 10f;
    [Tooltip("Particle effect for the booster (e.g., swirling vortex).")]
    public ParticleSystem boostEffect;
    
    [Header("Attraction Settings")]
    [Tooltip("Maximum horizontal distance from the player for trash to be attracted.")]
    public float horizontalAttractionDistance = 5f;
    
    private Transform player; // Automatically found using tag "Player".

    private void Start()
    {
        // Automatically find the player by tag.
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            Debug.Log("RecyclotronBooster: Player found: " + player.name);
        }
        else
        {
            Debug.LogError("RecyclotronBooster: Player not found! Ensure your player is tagged 'Player'.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("RecyclotronBooster: Player collected Recyclotron booster.");
            
            if (boostEffect != null)
            {
                boostEffect.Play();
                Debug.Log("RecyclotronBooster: Boost effect started.");
            }
            else
            {
                Debug.LogWarning("RecyclotronBooster: No boostEffect assigned.");
            }
            
            StartCoroutine(ActivateMagnet());
            Destroy(gameObject);
        }
    }

    private IEnumerator ActivateMagnet()
    {
        float elapsed = 0f;
        Debug.Log("RecyclotronBooster: Magnet effect activated for " + boostDuration + " seconds.");
        
        while (elapsed < boostDuration)
        {
            AttractTrash();
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        if (boostEffect != null)
        {
            boostEffect.Stop();
            Debug.Log("RecyclotronBooster: Boost effect stopped.");
        }
        Debug.Log("RecyclotronBooster: Magnet effect ended after " + boostDuration + " seconds.");
    }

    private void AttractTrash()
    {
        if (player == null)
        {
            Debug.LogError("RecyclotronBooster: Player reference is missing!");
            return;
        }
        
        // Use an OverlapSphere centered on the player's position.
        Collider[] colliders = Physics.OverlapSphere(player.position, magnetRange);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Trash"))
            {
                Rigidbody trashRb = col.GetComponent<Rigidbody>();
                if (trashRb != null)
                {
                    float horizontalDistance = Mathf.Abs(col.transform.position.x - player.position.x);
                    if (horizontalDistance <= horizontalAttractionDistance)
                    {
                        Vector3 direction = (player.position - col.transform.position).normalized;
                        // Use linearVelocity instead of velocity.
                        trashRb.linearVelocity = Vector3.Lerp(trashRb.linearVelocity, direction * attractionForce, Time.deltaTime * 5f);
                        Debug.Log("RecyclotronBooster: Attracting " + col.name + " with force " + attractionForce + " (horizontal distance: " + horizontalDistance + ").");
                    }
                    else
                    {
                        Debug.Log("RecyclotronBooster: " + col.name + " is too far horizontally (" + horizontalDistance + ") to be attracted.");
                    }
                }
                else
                {
                    Debug.LogWarning("RecyclotronBooster: " + col.name + " has no Rigidbody component.");
                }
            }
        }
    }
}
