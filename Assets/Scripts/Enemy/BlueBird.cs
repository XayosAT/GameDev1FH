using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBird : MonoBehaviour, IKillableEnemy {
    private Animator _animator;
    private readonly float _jumpingPower = 20f;
    public bool IsHit { get; set; }
    
    void Start() {
        _animator = GetComponent<Animator>();
    }
    
    public void InteractWithEnemy(GameObject player) {
        _animator.SetBool("IsHit", true);
        Rigidbody2D playerRb = player.gameObject.GetComponent<Rigidbody2D>();
        playerRb.velocity = new Vector2(playerRb.velocity.x, 0); // This line ensures the jump force is consistent
        playerRb.AddForce(new Vector2(0, _jumpingPower), ForceMode2D.Impulse);
        if (IsHit) return;
        IsHit = true;
        player.gameObject.GetComponent<PlayerStats>().AddEnemyKilled();
        StartCoroutine(PlayerKill());
    }

    private IEnumerator PlayerKill() {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}