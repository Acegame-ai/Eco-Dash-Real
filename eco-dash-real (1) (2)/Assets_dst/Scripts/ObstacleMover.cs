using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    private float moveSpeed;         // Speed at which the object moves
    private Transform player;        // Reference to the player's transform
    private float destroyDistance;   // Distance behind the player to destroy the object

    // Initialize the movement parameters
    public void Initialize(float speed, Transform playerTransform, float distance)
{
    moveSpeed = speed;
    player = playerTransform;
    destroyDistance = distance;

    Debug.Log($"Obstacle initialized: {gameObject.name}, Speed: {moveSpeed}");
}


    private void Update()
    {
        if (GameManager.Instance.currentState != GameState.Running) return;

        // Smoothly move the object backward at the given speed
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);

        // Destroy the object if it is too far behind the player
        if (player != null && player.position.z - transform.position.z > destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
