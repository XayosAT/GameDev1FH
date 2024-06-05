using UnityEngine;

public class SawMover : MonoBehaviour
{
    public float pointA; // The first point (left)
    public float pointB; // The second point (right)
    public float speed = 2.0f; // Speed of the saw

    private Vector2 targetPosition;

    void Start()
    {
        targetPosition = new Vector2(pointB, transform.position.y);
    }

    void Update()
    {
        MoveSaw();
    }

    void MoveSaw()
    {
        Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        transform.position = new Vector2(newPosition.x, transform.position.y);
        
        if (transform.position.x == pointA)
        {
            targetPosition = new Vector2(pointB, transform.position.y);
        }
        else if (transform.position.x == pointB)
        {
            targetPosition = new Vector2(pointA, transform.position.y);
        }
        // Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        // transform.position = new Vector3(newPosition.x, transform.position.y, transform.position.z);

        // if (transform.position.x == pointA.position.x)
        // {
        //     targetPosition = pointB.position;
        // }
        // else if (transform.position.x == pointB.position.x)
        // {
        //     targetPosition = pointA.position;
        // }
    }
}

