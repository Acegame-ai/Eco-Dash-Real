using UnityEngine;

public enum BoosterType
{
    SolarSprint,
    Recyclotron
}

public class BoosterPowerup : MonoBehaviour
{
    [Tooltip("Type of booster this power-up represents.")]
    public BoosterType boosterType;
    [Tooltip("Duration for which the player's aura will be active upon pickup.")]
    public float auraDuration = 5f;
    [Tooltip("Distance behind the player at which this booster will despawn.")]
    public float despawnDistance = 20f;

    private Transform playerTransform;

    private void Start()
    {
        // Cache the player's transform using the "Player" tag.
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
        else
        {
            Debug.LogError("BoosterPowerup: Player not found! Ensure your player is tagged 'Player'.");
        }
    }

    private void Update()
    {
        // If the booster is a certain distance behind the player, destroy it.
        if (playerTransform != null && transform.position.z < playerTransform.position.z - despawnDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Activate the booster effect on the player.
            PlayerBoosters boosters = other.GetComponent<PlayerBoosters>();
            if (boosters != null)
            {
                switch (boosterType)
                {
                    case BoosterType.SolarSprint:
                        boosters.ActivateSolarSprint();
                        break;
                    case BoosterType.Recyclotron:
                        boosters.ActivateRecyclotron();
                        break;
                }
            }

            // Activate the player's aura.
            PlayerAura aura = other.GetComponent<PlayerAura>();
            if (aura != null)
            {
                aura.ActivateAura(auraDuration);
            }

            // Destroy the booster after pickup.
            Destroy(gameObject);
        }
    }
}
