using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector3 startPos;
    public bool startDirRight = true;
    public float speed = 2f;
    public float distance = 5f;
    private float _horizontal;
    public GameObject coin;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;

        if (startDirRight)
        {
            _horizontal = 1;
            Flip();
        }
        else
        {
            _horizontal = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Turn();
        HandleMovement();
    }

    private void HandleMovement()
    {
        rb.velocity = new Vector2(_horizontal * speed, rb.velocity.y);
    }

    private void Turn()
    {
        Vector3 moved = transform.position - startPos;
        if (moved.x < 0 && moved.x <= -distance && _horizontal < 0)
        {
            _horizontal = 1;
            Flip();
        }
        else if (moved.x > 0 && moved.x >= distance && _horizontal > 0)
        {
            _horizontal = -1;
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void PlayerKill(bool spawnCoin)
    {
        if (spawnCoin)
        {
            Instantiate(coin, gameObject.transform.position, coin.transform.rotation);
        }
        Destroy(gameObject);
    }
}
