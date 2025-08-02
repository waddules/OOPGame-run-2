using UnityEngine;

public class FireballPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Get all weapon components
            PlayerAttack playerAttack = collision.GetComponent<PlayerAttack>();
            BowWeapon bowWeapon = collision.GetComponent<BowWeapon>();
            SwordWeapon swordWeapon = collision.GetComponent<SwordWeapon>();

            if (playerAttack != null)
            {
                // Disable all other weapons
                if (swordWeapon != null) swordWeapon.enabled = false;
                if (bowWeapon != null) bowWeapon.enabled = false;

                // Enable sword weapon
                playerAttack.enabled = true;

                // Destroy pickup
                Destroy(gameObject);

                Debug.Log("Staff equipped - other weapons disabled");
            }
        }
    }
}