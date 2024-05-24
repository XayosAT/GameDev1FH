using System.Collections;
using System.Collections.Generic;
using PlayerState;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerStateManager : MonoBehaviour
{
    //public Animator animator;
    
    
    [FormerlySerializedAs("_rb")] [HideInInspector] public Rigidbody2D rb;
    [FormerlySerializedAs("_playerStats")] [HideInInspector] public PlayerStats playerStats;
    [FormerlySerializedAs("_audioHandler")] [HideInInspector] public PlayerAudioHandler audioHandler;
    [FormerlySerializedAs("_playerAnim")] [HideInInspector] public Animator playerAnim;
    public Transform groundCheck;
    public Transform facingCheck;
    public LayerMask groundLayer;

    [FormerlySerializedAs("_horizontal")] [HideInInspector] public float horizontal;
    public float speed = 8f;
    public float jumpingPower = 12f;
    private bool _isFacingRight = true;
    public Vector2 boxSizeJump;
    public float castDistance;

    public  IPlayerState _currentState;
    public IPlayerState CurrentState { get { return _currentState; } set { _currentState = value; } }
    
    public IdleState idleState = new IdleState();
    public RunningState runningState = new RunningState();
    public JumpingState jumpingState = new JumpingState();
    public DamagedState damagedState = new DamagedState();
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
        playerAnim = GetComponent<Animator>();
        audioHandler = GetComponent<PlayerAudioHandler>();
        
        _currentState = idleState;
        _currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        CheckFacingDirection();
        _currentState.UpdateState(this);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _currentState.OnCollisionEnter(this, collision);
    }
    
    public bool IsGrounded()
    {
        return Physics2D.BoxCast(transform.position, boxSizeJump, 0, -transform.up, castDistance, groundLayer);

    }
    
    private void CheckFacingDirection()
    {
        if(!_isFacingRight && horizontal > 0f || _isFacingRight && horizontal < 0f)
        {
            Flip();
        }
    }
    
    public void SwitchState(IPlayerState newState)
    {
        CurrentState = newState;
        CurrentState.EnterState(this);
    }
    
    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
