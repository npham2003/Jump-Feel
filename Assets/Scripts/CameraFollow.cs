using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void LateUpdate()
    {
        if (player.position.y > transform.position.y)
        {
            Vector3 newPosition = new Vector3(transform.position.x, player.position.y, transform.position.z) + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, newPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
