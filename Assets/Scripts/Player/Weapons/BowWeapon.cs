using UnityEngine;

public class BowWeapon : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private bool isCharging = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // Start charging when mouse button is pressed
        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown)
        {
            isCharging = true;
            anim.SetBool("isCharging", true);
        }

        // Shoot when mouse button is released
        if (isCharging && Input.GetMouseButtonUp(0))
        {
            Attack();
            isCharging = false;
            anim.SetBool("isCharging", false);
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        arrows[FindArrows()].transform.position = firePoint.position;
        arrows[FindArrows()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindArrows()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}