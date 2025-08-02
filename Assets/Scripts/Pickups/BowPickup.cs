using UnityEngine;

public class BowPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Get all weapon components
            PlayerAttack playerAttack = collision.GetComponent<PlayerAttack>();
            BowWeapon bowWeapon = collision.GetComponent<BowWeapon>();
            SwordWeapon swordWeapon = collision.GetComponent<SwordWeapon>();

            if (bowWeapon != null)
            {
                // Disable all other weapons
                if (playerAttack != null) playerAttack.enabled = false;
                if (swordWeapon != null) swordWeapon.enabled = false;

                // Enable bow weapon
                bowWeapon.enabled = true;

                // Destroy pickup
                Destroy(gameObject);

                Debug.Log("Bow equipped - other weapons disabled");
            }
        }
    }
}
