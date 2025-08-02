using UnityEngine;

public class RockHeadTrap : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Transform targetPosition; // Drag a target position in the Inspector
    [SerializeField] private bool returnToStart = true;
    [SerializeField] private float returnDelay = 1f;

    private bool isActivated = false;
    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (isActivated)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);

            // If reached target position, stop and return if needed
            if (Vector3.Distance(transform.position, targetPosition.position) < 0.01f)
            {
                isActivated = false;

                if (returnToStart)
                {
                    StartCoroutine(ReturnToStart());
                }
            }
        }
    }

    public void ActivateTrap()
    {
        if (!isActivated)
        {
            isActivated = true;
        }
    }

    private System.Collections.IEnumerator ReturnToStart()
    {
        yield return new WaitForSeconds(returnDelay);

        // Move back to start
        while (Vector3.Distance(transform.position, startPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            yield return null;
        }
    }
}
