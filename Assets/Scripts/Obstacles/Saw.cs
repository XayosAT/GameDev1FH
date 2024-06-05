using UnityEngine;

public class SawMover : MonoBehaviour
{
    public float distance = 5.0f; // Distance the saw will travel
    public float speed = 2.0f; // Speed of the saw
    
    public float upForce = 6f;
    public float sideForce = 15f;
    public float knockbackDuration = 1f;

    private Vector2 _targetPosition;
    private float _initialPositionX;

    void Start()
    {
        _initialPositionX = transform.position.x;
        _targetPosition = new Vector2(_initialPositionX + distance, transform.position.y);
    }

    void Update()
    {
        MoveSaw();
    }

    void MoveSaw()
    {
        Vector2 newPosition = Vector2.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
        transform.position = new Vector2(newPosition.x, transform.position.y);

        if (Mathf.Approximately(transform.position.x, _initialPositionX))
        {
            _targetPosition = new Vector2(_initialPositionX + distance, transform.position.y);
        }
        else if (Mathf.Approximately(transform.position.x, _initialPositionX + distance))
        {
            _targetPosition = new Vector2(_initialPositionX, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            

            if (playerRb != null)
            {
                if (transform.position.x > other.transform.position.x)
                {
                    playerRb.AddForce(new Vector2(-sideForce, upForce), ForceMode2D.Impulse);
                }
                else
                {
                    playerRb.AddForce(new Vector2(sideForce, upForce), ForceMode2D.Impulse);
                }
            
                // Set knockback state
                playerMovement.SetKnockbackState(true);
                playerMovement.Invoke("ResetKnockbackState", knockbackDuration); // Adjust duration as needed
            }
            else
            {
                Debug.LogError("Rigidbody2D component not found on Player");
            }
        }
    }

    
}

