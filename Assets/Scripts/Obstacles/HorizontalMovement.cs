using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour, IMovement
{
    public float speed = 3f;
    private bool movingRight = true;
    private Vector3 startPos;
    public float distance;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        float leftBoundary = startPos.x - distance;

        if (transform.position.x >= startPos.x)
            movingRight = false;
        else if (transform.position.x <= leftBoundary)
            movingRight = true;

        transform.Translate((movingRight ? Vector3.right : Vector3.left) * (Time.deltaTime * speed));
    }
}

