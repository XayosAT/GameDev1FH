using System;
using UnityEngine;

public class RotateObstacle : MonoBehaviour
{
    private bool shouldRotate;
    private float platformLength;
    private Transform player;
    private BoxCollider2D platformCollider;
    private float heightCheckdistance = 0.1f;
    public float rotationMaxDegrees;
    public float rotationSpeed;
    public LayerMask playerLayer;
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shouldRotate = true;
            player = collision.gameObject.GetComponent<Transform>();
        }
    }

    private void Awake()
    {
        platformCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (shouldRotate)
        {
            RaycastHit2D hit = Physics2D.BoxCast(platformCollider.bounds.center, platformCollider.bounds.size, 0,
                Vector2.up, heightCheckdistance, playerLayer);

            if (!hit.collider)
            {
                player = null;
                shouldRotate = false;
                return;
            }
            
            Vector2 playerRelativePosition = transform.InverseTransformPoint(player.position);
            float rotationMultiplier = CalculationRotationMultiplier(playerRelativePosition);
            int rotationDirection = playerRelativePosition.x > 0 ? 1 : -1;
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                Quaternion.AngleAxis(rotationMaxDegrees * rotationDirection, -Vector3.forward), Time.deltaTime * rotationSpeed * rotationMultiplier);
        } else if (transform.rotation.eulerAngles.z != 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, Time.deltaTime * rotationSpeed);
        }
    }

    private float CalculationRotationMultiplier(Vector2 playerRelativePosition)
    {
        int rotationDirection = playerRelativePosition.x > 0 ? 1 : -1;
        float rotationMultiplier =
            Mathf.Abs(Mathf.Clamp((playerRelativePosition.x * 2 / platformLength) * rotationDirection, -1, 1));
        return rotationMultiplier;
    }
}