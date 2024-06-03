using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public GameObject bulletPrefab;

    private bool _damaged = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerStats = GetComponent<PlayerStats>();
        _playerAnim = GetComponent<Animator>();
        _audioHandler = GetComponent<PlayerAudioHandler>();
        StartCoroutine(SetHasAppeared());
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded() && _horizontal == 0)
        {
            _playerAnim.SetBool("IsRunning", false);
        }
        _playerAnim.SetFloat("yVelocity", _rb.velocity.y);
        CheckFacingDirection();
    }

    void FixedUpdate()
    {
        //Movement is handled in FixedUpdate because we are using physics
        HandleMovement();
    }

    private IEnumerator SetHasAppeared()
    {
        // Wait for the length of the appearing animation
        yield return new WaitForSeconds(_playerAnim.GetCurrentAnimatorStateInfo(0).length);
        _playerAnim.SetBool("hasAppeared", true);
    }

    private void HandleMovement()
    {
        if (!IsFacingWall() || IsGrounded())
        {
            float movespeed = speed;
            if (_damaged) movespeed = speed * 0.5f;

            _rb.velocity = new Vector2(_horizontal * movespeed, _rb.velocity.y);
        }
    }

    private void CheckFacingDirection()
    {
        if (!_isFacingRight && _horizontal > 0f || _isFacingRight && _horizontal < 0f)
        {
            Flip();
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded() && !_damaged)
        {
            _playerAnim.SetBool("IsRunning", false);
            //_playerAnim.SetTrigger("Jump_trig");
            _playerAnim.SetBool("IsJumping", true);
            _audioHandler.PlaySound("Jump");
            // Clear any existing vertical velocity and apply an impulse upwards
            _rb.velocity = new Vector2(_rb.velocity.x, 0); // This line ensures the jump force is consistent
            _rb.AddForce(new Vector2(0, jumpingPower), ForceMode2D.Impulse);
            _playerStats.AddJumped();
        }

        if (context.canceled && _rb.velocity.y > 0f)
        {
            //_playerAnim.SetBool("IsRunning", false);
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        //return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);

        //THIS IS ANOTHER WAY TO CHECK IF THE PLAYER IS GROUNDED, USING A BOXCAST
        if (Physics2D.BoxCast(transform.position, boxSizeJump, 0, -transform.up, castDistance, groundLayer))
        {
            _playerAnim.SetBool("IsRunning", true);
            _playerAnim.SetBool("IsJumping", false);
            return true;
        }
        if(!_damaged)
            _playerAnim.SetBool("IsJumping", true);
        return false;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSizeJump);
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
        if (IsGrounded() && _horizontal != 0 && !_damaged)
        {
            _playerAnim.SetBool("IsRunning", true);
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 spawnPosition = _isFacingRight ?
                new Vector2(transform.position.x + 1f, transform.position.y) :
                new Vector2(transform.position.x - 1f, transform.position.y);

            Quaternion spawnRot = Quaternion.identity;
            if (_isFacingRight)
            {
                spawnRot = Quaternion.Euler(bulletPrefab.transform.eulerAngles + new Vector3(0, 0, 180));
            }

            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, spawnRot);

            Vector2 direction = _isFacingRight ? Vector2.right : Vector2.left;

            Bullet bulletScript = bullet.GetComponent<Bullet>();

            if (bulletScript != null)
            {
                bulletScript.Initialize(direction, teamColor);
            }
        }
    }

    public void TakeDamage(float time)
    {
        if (_damaged) return;
        _playerAnim.SetBool("IsHit", true);
        _damaged = true;
        StartCoroutine(ResetToIdleAfterDelay(time));
    }

    private IEnumerator ResetToIdleAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        _damaged = false;
        _playerAnim.SetBool("IsHit", false);
    }
}
