using UnityEngine;

public class MoveWithEnvironment : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the object moves toward the player
    public float despawnDistance = -20f; // Distance behind the player to return to the pool

    private Transform environment; // Reference to the moving environment

    private void Start()
    {
        // Find the environment in the scene
        environment = GameObject.FindGameObjectWithTag("Road")?.transform;
        if (environment == null)
        {
            Debug.LogError("Environment not found! Ensure it is tagged correctly.");
        }
    }

    private void Update()
    {
        // Ensure the environment reference exists
        if (environment == null) return;

        // Move with the environment
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        // Despawn if it goes too far behind the player
        if (transform.position.z < environment.position.z + despawnDistance)
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        // Return the object to the pool
        ObjectPool.Instance.ReturnObject(gameObject.tag, this.gameObject);
    }
}
