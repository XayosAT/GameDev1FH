using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour, IMovement
{
    public float speed = 3f;
    private bool movingRight;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (transform.position.x >= 150) 
            movingRight = false;
        else if (transform.position.x <= 145) 
            movingRight = true;

        transform.Translate((movingRight ? Vector3.right : Vector3.left) * (Time.deltaTime * speed));
    }
}
