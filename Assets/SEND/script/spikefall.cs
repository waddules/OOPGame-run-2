    using UnityEngine;

    public class Spikefall : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private Transform targetPosition; // Drag the target point here
        [SerializeField] private float destroyDelay = 0.5f; // Time before disappearing
        [SerializeField] private int damage = 1;

        private bool isActivated = false;

        private void Update()
        {
            if (isActivated)
            {
                // Move towards target position
                transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);

                // Check if it reached the target
                if (Vector3.Distance(transform.position, targetPosition.position) < 0.01f)
                {
                    Destroy(gameObject, destroyDelay);
                }
            }
        }

        public void ActivateTrap()
        {
            isActivated = true;
        }

        
    }
