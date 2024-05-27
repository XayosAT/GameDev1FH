using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour, IKillableEnemy {
    private Animator _animator;
    private float jumpingPower = 7f;
    private bool isHit;
    void Start() {
        _animator = GetComponent<Animator>();
    }

    public void InteractWithEnemy(GameObject player) {
        _animator.SetBool("IsHit", true);
        Rigidbody2D playerRb = player.gameObject.GetComponent<Rigidbody2D>();
        playerRb.velocity = new Vector2(playerRb.velocity.x, 0); // This line ensures the jump force is consistent
        playerRb.AddForce(new Vector2(0, jumpingPower), ForceMode2D.Impulse);
        if (isHit) return;
        isHit = true;
        player.gameObject.GetComponent<PlayerStats>().AddEnemyKilled();
        StartCoroutine(PlayerKill());
    }

    private IEnumerator PlayerKill() {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}