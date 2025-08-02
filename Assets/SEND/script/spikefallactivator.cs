using UnityEngine;

public class SpikefallActivator : MonoBehaviour
{
    [SerializeField] private Spikefall spikeTrap; // Assign the Spikefall object here in the Inspector
    [SerializeField] private bool deactivateAfterUse = true; // Should the activator disable itself?

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spikeTrap.ActivateTrap(); // Call ActivateTrap() on the trap instance

            if (deactivateAfterUse)
            {
                gameObject.SetActive(false); // Disable activator so it can't be triggered again
            }
        }
    }
}
