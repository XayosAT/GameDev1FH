using UnityEngine;

public class Trampoline : MonoBehaviour, IObstacle
{
    public float jumpingPower = 15f;

    public void InteractWithObstacle(GameObject player)
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, jumpingPower);
    }
}
