using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private int damage = 1;  // damage amount

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Try to get the Health component instead of PlayerHealth
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Player touched trap!");
            }
        }
    }
}
