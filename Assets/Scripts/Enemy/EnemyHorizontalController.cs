using UnityEngine;

public class EnemyHorizontalController : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject coin;

    private Vector3 startPos;
    public bool startDirRight = true;
    public float speed = 2f;
    public float distance = 5f;
    public float _horizontal = -1;
    public bool SpawnCoinOnDeath;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;

        if (startDirRight)
        {
            Flip();
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
        if (moved.x < 0 && moved.x <= -distance && _horizontal < 0 ||
            moved.x > 0 && moved.x >= distance && _horizontal > 0)
        {
            Flip();
        }
    }

    public void Flip()
    {
        _horizontal = -_horizontal;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void PlayerKill()
    {
        if (SpawnCoinOnDeath)
        {
            Instantiate(coin, gameObject.transform.position, coin.transform.rotation);
        }
        Destroy(gameObject);
    }
}
