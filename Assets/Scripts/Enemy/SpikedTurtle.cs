using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedTurtle : MonoBehaviour, INotKillableEnemy {
    private Animator _animator;
    private EnemyHorizontalController _enemyHorizontalController;
    void Start() {
        _animator = GetComponent<Animator>();
        _enemyHorizontalController = GetComponent<EnemyHorizontalController>();
    }
    

    public void InteractWithPlayer(Rigidbody2D playerRb, Collision2D other) {
        Vector2 contactPoint = other.GetContact(0).point;
        Vector2 center = other.collider.bounds.center;

        if (contactPoint.x > center.x) {
            if (_enemyHorizontalController._horizontal > 0) {
                _enemyHorizontalController.Flip();
            }
            playerRb.AddForce(new Vector2(10f, 5f), ForceMode2D.Impulse);
        }
        else {
            if (_enemyHorizontalController._horizontal < 0) {
                _enemyHorizontalController.Flip();
            }
            playerRb.AddForce(new Vector2(-10f, 5f), ForceMode2D.Impulse);
        }
    }
}
