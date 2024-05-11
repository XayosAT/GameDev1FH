using UnityEngine;

public class EnemyHorizontalFlying : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector3 startPos;

    public float flyingPower = 3f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (transform.position.y < startPos.y)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(new Vector2(0, flyingPower), ForceMode2D.Impulse);
        }
    }
}
