using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkBullet : MonoBehaviour
{
    public float speed = 10f; // Speed of the bullet
    public float maxDistance = 20f; // Maximum distance the bullet can travel
    public float force = 20;
    public float knockbackDuration = 1.5f;

    private Vector2 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        // Move the bullet to the left
        transform.Translate(Vector2.left * (speed * Time.deltaTime));

        // Check the distance traveled
        float distanceTraveled = Vector2.Distance(_startPosition, transform.position);
        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject); // Destroy the bullet after it has traveled the maximum distance
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            

            if (playerRb != null)
            {
                
                playerRb.AddForce(new Vector2(-force, 5), ForceMode2D.Impulse);
                
            
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
