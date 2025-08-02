using UnityEngine;

public class CameraOffsetTrigger : MonoBehaviour
{
    [SerializeField] private float newXOffset = -2.5f;
    [SerializeField] private float newYOffset = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
            if (cam != null)
            {
                cam.SetCameraOffset(newXOffset, newYOffset);
            }
        }
    }
}
