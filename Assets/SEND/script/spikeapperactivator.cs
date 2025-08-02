using UnityEngine;

public class SpikeActivatorSpawn : MonoBehaviour
{
    [SerializeField] private SpikeTrap spikePrefab; // Prefab or reference to spike trap
    [SerializeField] private float offsetInFront = 1.5f; // Distance in front of player

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            float direction = Mathf.Sign(collision.transform.localScale.x);

            Vector3 spawnPos = collision.transform.position + new Vector3(direction * offsetInFront, 0, 0);

            SpikeTrap spike = Instantiate(spikePrefab, spawnPos, Quaternion.identity);
            spike.Appear(); // <---- FIXED!

            gameObject.SetActive(false); // Disable activator after use
        }
    }
}
