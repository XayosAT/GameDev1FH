using System.Collections;
using UnityEngine;

public class RockHeadSpikeHeadController : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject coin;

    public float reactSpeed = 10f;
    public float returnSpeed = 4f;
    public bool moveHorizontally = false;
    public bool movePositiveDirection = true;
    public float startPosTolerance = 0.5f;
    public bool SpawnCoinOnDeath;
    public float restAtStartPoint = 2.5f;
    private Vector3 startPos;
    private bool isReturning = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;

        StartMove();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(StartPosReached());
    }

    IEnumerator StartPosReached()
    {
        if (isReturning &&
            transform.position.x >= (startPos.x - startPosTolerance) &&
            transform.position.x <= (startPos.x + startPosTolerance) &&
            transform.position.y >= (startPos.y - startPosTolerance) &&
            transform.position.y <= (startPos.y + startPosTolerance))
        {
            rb.velocity = Vector3.zero;
            isReturning = false;
            yield return new WaitForSeconds(restAtStartPoint);
            StartMove();
        }
    }

    private void StartMove()
    {
        if (moveHorizontally)
        {
            if (movePositiveDirection)
            {
                rb.velocity = new Vector2(reactSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-reactSpeed, rb.velocity.y);
            }
        }
        else
        {
            if (movePositiveDirection)
            {
                rb.velocity = new Vector2(rb.velocity.x, reactSpeed);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, -reactSpeed);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        rb.velocity = Vector3.zero;
        StartCoroutine(ReturnToStart());
    }

    IEnumerator ReturnToStart()
    {
        isReturning = true;
        yield return new WaitForSeconds(0);
        if (moveHorizontally)
        {
            if (transform.position.x > startPos.x)
            {
                rb.velocity = new Vector2(-returnSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(returnSpeed, rb.velocity.y);
            }
        }
        else
        {
            if (transform.position.y > startPos.y)
            {
                rb.velocity = new Vector2(rb.velocity.x, -returnSpeed);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, returnSpeed);
            }
        }
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
