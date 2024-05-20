using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerStats _playerStats;
    private PlayerAudioHandler _audioHandler;
    private Animator _playerAnim;
    public Transform groundCheck;
    public Transform facingCheck;
    public LayerMask groundLayer;

    private float _horizontal;
    public float speed = 8f;
    public float jumpingPower = 12f;
    private bool _isFacingRight = true;
    public Vector2 boxSizeJump;
    public float castDistance;
    public TeamColor teamColor;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerStats = GetComponent<PlayerStats>();
        _playerAnim = GetComponent<Animator>();
        _audioHandler = GetComponent<PlayerAudioHandler>();
    }

    // Update is called once per frame
    void Update()
    {
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
            _rb.velocity = new Vector2(_horizontal * speed, _rb.velocity.y);
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
            _playerAnim.SetTrigger("Jump_trig");
            _audioHandler.PlaySound("Jump");
            // Clear any existing vertical velocity and apply an impulse upwards
            _rb.velocity = new Vector2(_rb.velocity.x, 0); // This line ensures the jump force is consistent
            _rb.AddForce(new Vector2(0, jumpingPower), ForceMode2D.Impulse);

            _playerStats.AddJumped();
        }

        if(context.canceled && _rb.velocity.y > 0f)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        //return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
        
        //THIS IS ANOTHER WAY TO CHECK IF THE PLAYER IS GROUNDED, USING A BOXCAST
        if (Physics2D.BoxCast(transform.position, boxSizeJump, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        return false;
        
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - transform.up*castDistance, boxSizeJump);
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
