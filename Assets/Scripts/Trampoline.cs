using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpingPower = 15f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerJump(Rigidbody2D playerRb)
    {
        playerRb.velocity = new Vector2(playerRb.velocity.x, 0); // This line ensures the jump force is consistent
        playerRb.AddForce(new Vector2(0, jumpingPower), ForceMode2D.Impulse);
    }
}
