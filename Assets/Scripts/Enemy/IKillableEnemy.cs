
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public interface IKillableEnemy {
    void InteractWithEnemy(GameObject player, Collision2D collision2D);
}