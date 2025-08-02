using UnityEngine;

public class TrapActivator : MonoBehaviour
{
    [SerializeField] private RockHeadTrap rockHeadTrap;
    [SerializeField] private float delay = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ActivateTrapAfterDelay());
        }
    }

    private System.Collections.IEnumerator ActivateTrapAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        if (rockHeadTrap != null)
        {
            rockHeadTrap.ActivateTrap();
        }
    }
}
