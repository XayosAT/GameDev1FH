
using UnityEngine;

public interface INotKillableEnemy {
    void InteractWithPlayer(Rigidbody2D playerRb, Collision2D other);
}
