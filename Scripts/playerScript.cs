using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    private float facingDir;

    [Header("Movement")]
    public float moveSpeed = 10f;
    private float moveDir = 1;
    private float baseMoveSpeed;

    [Header("Jumping")]
    public float jumpForce = 10f;
    public float doubleJumpForce = 7f;
    private float jumpTimeCounter;
    public float jumpTime = 0.3f;
    public int baseJumpCount = 1;
    public int jumpCount;
    private float baseJumpForce;

    [Header("Dashing")]
    public float dashDistance = 25f;
    public float dashTime = 0.15f;
    public float startDashesLeft = 0;
    private float dashesLeft;
    public float dashStartCooldown;
    private float dashCooldown;

    [Header("Sliding")]
    public float slideTime = 0.1f;
    private float slideTimeCounter;
    public float speedMultiplier = 1.5f;
    public float jumpMultiplier = 1.5f;
    public float startBeforeSlide;
    public float movementBeforeSlide;

    private bool isDashing;
    private bool canDash;
    private bool isJumping;
    private bool isGrounded;
    private bool canSlide;

    private float gravity;

    private GameManager gameManager;
    private Rigidbody2D rb;
    private Animator animator;

    [Header("GroundCheck")]
    public Transform feetPos;
    public LayerMask whatIsGround;
    public float checkRadius;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gravity = rb.gravityScale;
        jumpCount = baseJumpCount;
        isDashing = false;
        dashCooldown = 0f;
        slideTimeCounter = slideTime;
        baseMoveSpeed = moveSpeed;
        baseJumpForce = jumpForce;
        movementBeforeSlide = startBeforeSlide;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerJumping();
        FlipPlayer();
        PlayerDash();
        
        if (isGrounded)
        {
            dashesLeft = startDashesLeft;
            jumpCount = baseJumpCount;
            //PlayerSlide();
            //CanSlide();
        }
    }

    private void FixedUpdate()
    { 
        if (isDashing == false)
        {
            PlayerMovement();
        }
    }

    private void PlayerMovement()
    {
        moveDir = Input.GetAxisRaw("Horizontal");
        if (moveDir == 1)
        {
            facingDir = 1;
        }
        else if (moveDir == -1)
        {
            facingDir = -1;
        }
        if (moveDir != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        rb.velocity = new Vector2(moveDir * moveSpeed * Time.deltaTime, rb.velocity.y);
    }

    private void PlayerDash()
    {   
        if (dashesLeft >= 0)
        {
            canDash = true;
        }
        else
        {
            canDash = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldown <= 0 && canDash)
        {
            StartCoroutine(Dash(facingDir));
            dashCooldown = dashStartCooldown;
            dashesLeft--;
        }
        if (dashCooldown >= 0)
        {
            dashCooldown -= Time.deltaTime;
        }
    }

    IEnumerator Dash(float direction)
    {
        isDashing = true;
        rb.velocity = new Vector2(0f, 0f);
        rb.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        rb.gravityScale = 0;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        rb.gravityScale = gravity;
    }


    private void CanSlide()
    {
        if (rb.velocity.x != 0)
        {
            movementBeforeSlide -= Time.deltaTime;
        }
        else if (rb.velocity.x == 0)
        {
            movementBeforeSlide = startBeforeSlide;
        }
        if (movementBeforeSlide <= 0)
        {
            canSlide = true;
        }
        else
        {
            canSlide = false;
        }
    }

    private void PlayerSlide()
    {
        if (canSlide)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.localScale = new Vector3(1, 0.5f, 1);
                moveSpeed *= speedMultiplier;
                jumpForce *= jumpMultiplier;
            }
            if (Input.GetKey(KeyCode.S) && slideTimeCounter >= 0)
            {
                slideTimeCounter -= Time.deltaTime;
            }
        }
        
        if (Input.GetKeyUp(KeyCode.S) || slideTimeCounter <= 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            moveSpeed = baseMoveSpeed;
            jumpForce = baseJumpForce;
            slideTimeCounter = slideTime;
            Debug.Log(baseMoveSpeed + "baseMove ... baseJump" + baseJumpForce);
        }
    }

    private void PlayerJumping()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space) || jumpCount > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            jumpCount--;
        }

        if (jumpCount <= 0)
        {
            jumpForce = doubleJumpForce;
        }
        else
        {
            jumpForce = baseJumpForce;
        }


        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Player" && collision.gameObject.tag == "Hazard")
        {
            gameManager.GameOver();
        }

        if (collision.gameObject.tag == "VictoryPoint")
        {
            gameManager.Victory();
        }
    }

    private void FlipPlayer()
    {
        if (moveDir > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveDir < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        
    }
}
