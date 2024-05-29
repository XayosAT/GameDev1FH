
using UnityEngine;

public interface IKillableEnemy {
    bool IsHit { get; set; }
    void InteractWithPlayerFoot(GameObject player);
    void InteractWithPlayer(Rigidbody2D playerRb, Collision2D other);
}