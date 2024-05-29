using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, IKillableEnemy {
    private Animator _animator;
    private readonly float _jumpingPower = 10f;
    private bool _isHit;
    private Vector2 _contactPoint;
    private Vector2 _center;
    
    void Start() {
        _animator = GetComponent<Animator>();
    }

    public void InteractWithEnemy(GameObject player, Collision2D collision2D) {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (KilledByPlayer(collision2D)) {
            _animator.SetBool("IsHit", true);
            Rigidbody2D playerRb = player.gameObject.GetComponent<Rigidbody2D>();
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0); // This line ensures the jump force is consistent
            playerRb.AddForce(new Vector2(0, _jumpingPower), ForceMode2D.Impulse);
            if (_isHit) return;
            _isHit = true;
            player.gameObject.GetComponent<PlayerStats>().AddEnemyKilled();
            StartCoroutine(PlayerKill());
        }
        else {
            //Vector2 pushDirection = new Vector2(Mathf.Cos(Mathf.PI / 4), Mathf.Sin(Mathf.PI / 4));
            //Vector2 pushDirection = new Vector2(Mathf.Sqrt(2) / 2, 0);
            float knockbackForce = 10f;
            Vector2 pushDirection = Vector2.right;
            Vector2 difference = (player.transform.position - transform.position).normalized;
            Vector2 force = difference * knockbackForce;
            
            if (_contactPoint.x <= _center.x) {
                //pushDirection.x = -Mathf.Abs(pushDirection.x);
                pushDirection.x = -pushDirection.x;
                Debug.Log($"Left {pushDirection.x} / {pushDirection.y}");
            }
            else {
                //pushDirection.x = Mathf.Abs(pushDirection.x);
                Debug.Log($"Right {pushDirection.x} / {pushDirection.y}");
            }

            // Apply the force to the Rigidbody2D
            //rb.AddForce(pushDirection * 100f, ForceMode2D.Impulse);
            //player.gameObject.GetComponent<PlayerMovement>().TakeDamage();
            //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            
            //rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

    private bool KilledByPlayer(Collision2D collision2D) {
        _contactPoint = collision2D.GetContact(0).point;
        _center = collision2D.collider.bounds.center;

        if (_contactPoint.y > _center.y + 0.4) {
            return true;
        }

        return false;
    }

    private IEnumerator PlayerKill() {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}