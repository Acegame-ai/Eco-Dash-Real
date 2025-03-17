using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject

    // Position offsets
    public float cameraXPos = 0;
    public float cameraYPos = 0;
    public float cameraZPos = 0;

    // Rotation offsets
    public float cameraXRot = 0;
    public float cameraYRot = 0;
    public float cameraZRot = 0;

    private void LateUpdate()
    {
        // Update the position of the camera
        Vector3 newPosition = player.transform.position + new Vector3(cameraXPos, cameraYPos, cameraZPos);
        transform.position = newPosition;

        // Update the rotation of the camera
        Quaternion newRotation = Quaternion.Euler(cameraXRot, cameraYRot, cameraZRot);
        transform.rotation = newRotation;
    }
}
