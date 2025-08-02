using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("Crouch Settings")]
    [SerializeField] private float crouchHeight = 0.5f; // Reduced collider height when crouching
    [SerializeField] private float crouchSpeedMultiplier = 0.5f; // Speed reduction when crouching
    [SerializeField] private KeyCode crouchKey = KeyCode.P;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    public CoinManager cm;
    private float originalHeight;
    private bool isCrouching;
    private Vector3 originalScale;

    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        originalHeight = boxCollider.size.y;
        originalScale = transform.localScale;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(originalScale.x, transform.localScale.y, originalScale.z);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-originalScale.x, transform.localScale.y, originalScale.z);

        if (Input.GetKeyDown(crouchKey) && isGrounded())
        {
            StartCrouch();
        }
        else if (Input.GetKeyDown(crouchKey))
        {
            StopCrouch();
        }

        //Set animator parameters
        anim.SetBool("run", horizontalInput != 0 && !isCrouching);
        anim.SetBool("grounded", isGrounded());
        anim.SetBool("crouch", isCrouching);

        //Wall jump logic
        if (wallJumpCooldown > 0.2f && !isCrouching)
        {
            float currentSpeed = isCrouching ? speed * crouchSpeedMultiplier : speed;
            body.linearVelocity = new Vector2(horizontalInput * currentSpeed, body.linearVelocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.linearVelocity = Vector2.zero;
            }
            else
                body.gravityScale = 7;

            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else
            wallJumpCooldown += Time.deltaTime;
    }

    private void StartCrouch()
    {
        isCrouching = true;
        float heightDifference = originalHeight - crouchHeight;
        boxCollider.size = new Vector2(boxCollider.size.x, crouchHeight);
        boxCollider.offset = new Vector2(boxCollider.offset.x, -heightDifference / 2f); // Center the collider vertically
    }

    private void StopCrouch()
    {
        // Check for enough space to stand up
        if (!Physics2D.BoxCast(
            boxCollider.bounds.center,
            new Vector2(boxCollider.size.x, originalHeight),
            0f, Vector2.up, 0.1f, groundLayer))
        {
            isCrouching = false;
            boxCollider.size = new Vector2(boxCollider.size.x, originalHeight);
            boxCollider.offset = new Vector2(boxCollider.offset.x, 0f); // Reset offset
        }
    }

    private void Jump()
    {
        if (isGrounded() && !isCrouching)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            wallJumpCooldown = 0;
        }
    }


    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall() && !isCrouching;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            cm.coinCount++;
            Destroy(collision.gameObject);
        }
    }
}