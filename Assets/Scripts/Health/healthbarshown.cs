using UnityEngine;

public class HealthBarShown : MonoBehaviour
{
    // A reference to the GameObject that holds the health bar UI
    [SerializeField] private GameObject Healthbar1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Activate the health bar UI
            if (Healthbar1 != null)
            {
                Healthbar1.SetActive(true);
            }
            if (Healthbar1 == null)
            {
                Healthbar1.SetActive(false);
            }
        }
    }
}