using UnityEngine;

public class HiddenTrap : MonoBehaviour
{
    [SerializeField] private float revealDelay = 0.5f;    // Delay before it deals damage
    [SerializeField] private int damage = 1;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D damageCollider;   // Collider for dealing damage

    private bool isRevealed = false;

    private void Awake()
    {
        // Hide trap and disable collider initially
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if (damageCollider != null) damageCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isRevealed && collision.CompareTag("Player"))
        {
            StartCoroutine(RevealTrap(collision.gameObject));
        }
    }

    private System.Collections.IEnumerator RevealTrap(GameObject player)
    {
        isRevealed = true;

        // Show trap visually
        if (spriteRenderer != null) spriteRenderer.enabled = true;

        // Wait before dealing damage
        yield return new WaitForSeconds(revealDelay);

        // Enable damage collider so player gets hurt
        if (damageCollider != null)
        {
            damageCollider.enabled = true;
        }

        // OPTIONAL: Immediately deal damage to player script (if you have Health system)
        // player.GetComponent<PlayerHealth>()?.TakeDamage(damage);
    }
}
