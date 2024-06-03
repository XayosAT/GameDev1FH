using System.Collections;
using UnityEngine;

public class Ghost : MonoBehaviour, INotKillableEnemy {
    private Animator _animator;
    private EnemyHorizontalController _enemyHorizontalController;
    
    void Start() {
        _animator = GetComponent<Animator>();
        _enemyHorizontalController = GetComponent<EnemyHorizontalController>();
    }

    public void InteractWithPlayer(Rigidbody2D playerRb, Collision2D other) {
        Vector2 contactPoint = other.GetContact(0).point;
        Vector2 center = other.collider.bounds.center;
        _animator.SetBool("IsHit", true);
        StartCoroutine(Disappear());
        if (contactPoint.y < center.y) {
            playerRb.AddForce(new Vector2(0, -2f), ForceMode2D.Impulse);
            return;
        }
        if (contactPoint.x > center.x) {
            playerRb.AddForce(new Vector2(10f, 5f), ForceMode2D.Impulse);
        }
        else {
            playerRb.AddForce(new Vector2(-10f, 5f), ForceMode2D.Impulse);
        }
    }

    private IEnumerator Disappear() {
        yield return new WaitForSeconds(0.1f);
        _animator.SetBool("IsHit", false);
    }
}
