using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform; // The transform of the player object

    void Update()
    {
        // Update the camera's position to match the player's position
        transform.position = new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);
    }
}
