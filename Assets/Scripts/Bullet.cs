using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    private Rigidbody2D _rb;
    private TeamColor _teamColor;
    private Vector2 _direction;
    private Vector2 _initialPosition;
    public float maxDistance = 10f; // Maximum distance the bullet can travel
    private float _distanceTraveled;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _initialPosition = transform.position;
        _rb.velocity = _direction * speed;
    }

    public void Initialize(Vector2 direction, TeamColor color)
    {
        _direction = direction;
        _teamColor = color;
    }

    // Update is called once per frame
    void Update()
    {
        _distanceTraveled = Vector2.Distance(_initialPosition, transform.position);
        if (_distanceTraveled >= maxDistance)
        {
            Destroy(gameObject); // Destroy the bullet after it has traveled the maximum distance
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerMovement>().teamColor != _teamColor)
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeDamage(1.5f);
                Destroy(gameObject);
            }
        }
        
    }
}
