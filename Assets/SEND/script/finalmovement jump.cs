using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 12f;

    [Header("Wall Jump")]
    [SerializeField] private float wallSlideSpeed = 2f;
    [SerializeField] private float wallJumpX = 8f;
    [SerializeField] private float wallJumpY = 12f;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D rb;
    private PolygonCollider2D polyCollider;
    private Vector3 originalScale;

    private bool isWallSliding;
    private CameraFollow camFollow;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        polyCollider = GetComponent<PolygonCollider2D>();
        rb.freezeRotation = true;
        originalScale = transform.localScale;
    }

    private void Start()
    {
        camFollow = Camera.main.GetComponent<CameraFollow>();
    }

    private void Update()
    {
        // Horizontal Movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

      

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        }

        // Wall Slide
        if (OnWall() && !IsGrounded() && rb.linearVelocity.y < 0)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlideSpeed);
        }
        else
        {
            isWallSliding = false;
        }

        // Wall Jump
        if (isWallSliding && Input.GetKeyDown(KeyCode.Space))
        {
            float direction = IsTouchingLeftWall() ? 1 : -1;
            rb.linearVelocity = new Vector2(direction * wallJumpX, wallJumpY);

            // Flip the player sprite direction
            transform.localScale = new Vector3(originalScale.x * direction, originalScale.y, originalScale.z);
        }
    }

    // Ground Check
    private bool IsGrounded()
    {
        Vector2 size = new Vector2(polyCollider.bounds.size.x * 0.9f, 0.1f);
        Vector2 origin = new Vector2(polyCollider.bounds.center.x, polyCollider.bounds.min.y - 0.05f);

        RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0f, Vector2.down, 0.05f, groundLayer);
        return hit.collider != null;
    }

    // Wall Check
    private bool OnWall()
    {
        return IsTouchingLeftWall() || IsTouchingRightWall();
    }

    private bool IsTouchingLeftWall()
    {
        return Physics2D.BoxCast(polyCollider.bounds.center, polyCollider.bounds.size, 0f, Vector2.left, 0.1f, wallLayer);
    }

    private bool IsTouchingRightWall()
    {
        return Physics2D.BoxCast(polyCollider.bounds.center, polyCollider.bounds.size, 0f, Vector2.right, 0.1f, wallLayer);
    }
}
