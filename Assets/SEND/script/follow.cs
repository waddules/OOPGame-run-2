using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;     // Player to follow
    [SerializeField] private float yOffset = 1f;   // Vertical offset
    [SerializeField] private float xOffset = 2.5f; // Horizontal offset
    [SerializeField] private float smoothSpeed = 5f;

    private void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPos = new Vector3(player.position.x + xOffset,
                                        player.position.y + yOffset,
                                        transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
    }

    // Change both X and Y offset
    public void SetCameraOffset(float newXOffset, float newYOffset)
    {
        xOffset = newXOffset;
        yOffset = newYOffset;
    }
}
