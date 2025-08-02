using UnityEngine;

public class PlayerWallJump : MonoBehaviour
{
    [SerializeField] private float wallSlideSpeed = 2f;
    [SerializeField] private float wallJumpX = 8f;
    [SerializeField] private float wallJumpY = 12f;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D rb;
    private PolygonCollider2D polyCollider;
    private Vector3 originalScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        polyCollider = GetComponent<PolygonCollider2D>();
        originalScale = transform.localScale;
    }

    private void Update()
    {
        // Wall slide
        if (OnWall() && rb.linearVelocity.y < 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlideSpeed);
        }

        // Wall jump
        if (OnWall() && Input.GetKeyDown(KeyCode.Space))
        {
            float direction = (IsTouchingLeftWall()) ? 1 : -1;
            rb.linearVelocity = new Vector2(direction * wallJumpX, wallJumpY);

            // Keep original scale, just flip
            transform.localScale = new Vector3(originalScale.x * direction, originalScale.y, originalScale.z);
        }
    }

    private bool OnWall()
    {
        return IsTouchingLeftWall() || IsTouchingRightWall();
    }

    private bool IsTouchingLeftWall()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            polyCollider.bounds.center,
            polyCollider.bounds.size,
            0f,
            Vector2.left,
            0.1f,
            wallLayer
        );
        return hit.collider != null;
    }

    private bool IsTouchingRightWall()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            polyCollider.bounds.center,
            polyCollider.bounds.size,
            0f,
            Vector2.right,
            0.1f,
            wallLayer
        );
        return hit.collider != null;
    }
}
