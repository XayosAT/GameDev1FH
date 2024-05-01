using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerStats playerStats;
    private Animator playerAnim;
    public Transform groundCheck;
    public Transform facingCheck;
    public LayerMask groundLayer;

    private float _horizontal;
    public float _speed = 3f;
    public float _jumpingPower = 5.5f;
    private bool _isFacingRight = true;
    public Vector2 boxSize;
    public float castDistance;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        CheckFacingDirection();
    }
    
    void FixedUpdate()
    {
        //Movement is handled in FixedUpdate because we are using physics
        HandleMovement();
    }
    
    private void HandleMovement()
    {
        if(!IsFacingWall() || IsGrounded())
        {
            rb.velocity = new Vector2(_horizontal * _speed, rb.velocity.y);
        }
    }
    
    private void CheckFacingDirection()
    {
        if(!_isFacingRight && _horizontal > 0f || _isFacingRight && _horizontal < 0f)
        {
            Flip();
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded())
        {
            playerAnim.SetTrigger("Jump_trig");

            //JUST TESTING DIFFERENT WAYS TO JUMP, BOTH ARE SIMILAR, IF THERE'S EVEN A DIFFERENCE
            // Clear any existing vertical velocity and apply an impulse upwards
            rb.velocity = new Vector2(rb.velocity.x, 0); // This line ensures the jump force is consistent
            rb.AddForce(new Vector2(0, _jumpingPower), ForceMode2D.Impulse);


            //rb.velocity = new Vector2(rb.velocity.x, _jumpingPower);

            playerStats.AddJumped();
        }

        if(context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        
        //return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
        
        //THIS IS ANOTHER WAY TO CHECK IF THE PLAYER IS GROUNDED, USING A BOXCAST
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        return false;
        
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - transform.up*castDistance, boxSize);
    }
    

    private bool IsFacingWall()
    {
        return Physics2D.OverlapCircle(facingCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        _horizontal = context.ReadValue<Vector2>().x;
    }
}
