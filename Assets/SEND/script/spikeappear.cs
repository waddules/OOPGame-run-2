using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private float riseSpeed = 10f;
    [SerializeField] private float riseDistance = 1f;      // How far up it should rise
    [SerializeField] private float visibleDuration = 0.5f; // How long to stay before disappearing
    [SerializeField] private int damage = 1;

    private bool isActivated = false;
    private Vector3 startPosition;
    private Vector3 endPosition;

    private void Awake()
    {
        // Record positions but DON'T deactivate here
        startPosition = transform.position;
        endPosition = transform.position + Vector3.up * riseDistance;

        // Start hidden below ground
        transform.position = startPosition;
    }

    private void Update()
    {
        if (isActivated)
        {
            // Move upwards
            transform.position = Vector3.MoveTowards(transform.position, endPosition, riseSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, endPosition) < 0.01f)
            {
                // Destroy after visible duration
                Destroy(gameObject, visibleDuration);
            }
        }
    }

    // Called by SpikeActivatorSpawn
    public void Appear()
    {
        isActivated = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActivated && collision.CompareTag("Player"))
        {
            // Damage player if health system exists
            // collision.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }
    }
}
