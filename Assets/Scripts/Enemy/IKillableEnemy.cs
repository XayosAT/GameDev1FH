
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public interface IKillableEnemy {
    bool IsHit { get; set; }
    void InteractWithEnemy(GameObject player);
    //void InteractWithEnemy(GameObject player);
}