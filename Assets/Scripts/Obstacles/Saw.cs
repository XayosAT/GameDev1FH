using UnityEngine;

public class SawMover : MonoBehaviour
{
    public Vector2 startPoint; // The start point position
    public Vector2 endPoint; // The end point position
    public float speed = 2.0f; // Speed of the saw
    
    public float upForce = 6f;
    public float sideForce = 15f;
    public float knockbackDuration = 1f;

    private Vector2 _targetPosition;

    void Start()
    {
        transform.position = startPoint;
        _targetPosition = endPoint;
    }

    void Update()
    {
        MoveSaw();
    }

    void MoveSaw()
    {
        Vector2 newPosition = Vector2.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

        if (Vector2.Distance(transform.position, startPoint) < 0.01f)
        {
            _targetPosition = endPoint;
        }
        else if (Vector2.Distance(transform.position, endPoint) < 0.01f)
        {
            _targetPosition = startPoint;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            GetComponent<Collider2D>().enabled = false;
            if (playerRb != null)
            {
                playerMovement.TakeDamage(1f);
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
                playerRb.AddForce(new Vector2(knockbackDirection.x * sideForce, upForce), ForceMode2D.Impulse);
            
                // Set knockback state
                playerMovement.SetKnockbackState(true);
                playerMovement.Invoke("ResetKnockbackState", knockbackDuration); // Adjust duration as needed
            }
            else
            {
                Debug.LogError("Rigidbody2D component not found on Player");
            }
            GetComponent<Collider2D>().enabled = true;

        }
    }
}