using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour, IKillableEnemy {
    private Animator _animator;
    private readonly float _jumpingPower = 7f;
    private EnemyHorizontalController _enemyHorizontalController;
    public bool IsHit { get; set; }
    
    void Start() {
        _animator = GetComponent<Animator>();
        _enemyHorizontalController = GetComponent<EnemyHorizontalController>();
    }
    
    public void InteractWithPlayerFoot(GameObject player) {
        _animator.SetBool("IsHit", true);
        Rigidbody2D playerRb = player.gameObject.GetComponent<Rigidbody2D>();
        playerRb.velocity = new Vector2(playerRb.velocity.x, 0); // This line ensures the jump force is consistent
        playerRb.AddForce(new Vector2(0, _jumpingPower), ForceMode2D.Impulse);
        if (IsHit) return;
        IsHit = true;
        player.gameObject.GetComponent<PlayerStats>().AddEnemyKilled();
        StartCoroutine(PlayerKill());
    }

    public void InteractWithPlayer(Rigidbody2D playerRb, Collision2D other) {
        Vector2 contactPoint = other.GetContact(0).point;
        Vector2 center = other.collider.bounds.center;
        bool flip = false;
        //knockback to right by 45 degrees
        if (contactPoint.x > center.x) {
            if (_enemyHorizontalController._horizontal > 0) {
                flip = true;
            }
            playerRb.AddForce(new Vector2(10f, 5f), ForceMode2D.Impulse);
        }
        else {
            if (_enemyHorizontalController._horizontal < 0) {
                flip = true;
            }
            playerRb.AddForce(new Vector2(-10f, 5f), ForceMode2D.Impulse);
        }

        if (flip) {
            _enemyHorizontalController.Flip();
        }
    }

    private IEnumerator PlayerKill() {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}