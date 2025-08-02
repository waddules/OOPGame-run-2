using UnityEngine;

public class SwordPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Get all weapon components
            PlayerAttack playerAttack = collision.GetComponent<PlayerAttack>();
            BowWeapon bowWeapon = collision.GetComponent<BowWeapon>();
            SwordWeapon swordWeapon = collision.GetComponent<SwordWeapon>();

            if (swordWeapon != null)
            {
                // Disable all other weapons
                if (playerAttack != null) playerAttack.enabled = false;
                if (bowWeapon != null) bowWeapon.enabled = false;

                // Enable sword weapon
                swordWeapon.enabled = true;

                // Destroy pickup
                Destroy(gameObject);

                Debug.Log("Sword equipped - other weapons disabled");
            }
        }
    }
}
