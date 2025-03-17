using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    private float moveSpeed;       // Speed provided by the environment
    private Transform player;      // Reference to the player's transform
    private float destroyDistance; // Distance behind the player at which the obstacle is destroyed

    /// <summary>
    /// Initializes the obstacle with the provided speed, player reference, and destroy distance.
    /// </summary>
    /// <param name="speed">The speed at which the obstacle should move.</param>
    /// <param name="playerTransform">Reference to the player's transform.</param>
    /// <param name="distance">Distance behind the player at which the obstacle is destroyed.</param>
    public void Initialize(float speed, Transform playerTransform, float distance)
    {
        moveSpeed = speed;
        player = playerTransform;
        destroyDistance = distance;

        Debug.Log($"ObstacleMover: {gameObject.name} initialized with speed: {moveSpeed}, destroyDistance: {destroyDistance}");
    }

    private void Update()
    {
        // Only move if the game is running.
        if (GameManager.Instance.currentState != GameState.Running)
            return;

        // Move the obstacle backward at the provided moveSpeed.
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);

        // Check if the obstacle has fallen too far behind the player.
        if (player != null && (player.position.z - transform.position.z) > destroyDistance)
        {
            Debug.Log($"ObstacleMover: {gameObject.name} is behind the player by more than {destroyDistance}. Destroying it.");
            Destroy(gameObject);
        }
    }
}
