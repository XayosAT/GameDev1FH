using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /*public PlayerControls inputActions;
    private float moveInput;
    private float speed = 2.0f;
    private float movementX;
    private float movementY;*/
    private Rigidbody2D rb;
    private float horizontalInput;
    private float speed = 5.0f;
    private float jumpForce = 4;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    /*private void FixedUpdate()
    {
        /*rb.velocity = new UnityEngine.Vector2(moveInput * speed, rb.velocity.y);
        Debug.Log(moveInput);*//*

        UnityEngine.Vector3 movement = new UnityEngine.Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }


    private void OnMove(InputValue movementValue)
    {
        UnityEngine.Vector2 movementVector = movementValue.Get<UnityEngine.Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }*/

    /*private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMovePerformed;
        inputActions.Player.Move.performed += OnMoveCancelled;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMovePerformed;
        inputActions.Player.Move.performed -= OnMoveCancelled;
        inputActions.Player.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<float>();
        rb.AddForce( * speed);
    }

    private void OnMoveCancelled(InputAction.CallbackContext context)
    {
        //moveInput = 0;
    }*/
}
