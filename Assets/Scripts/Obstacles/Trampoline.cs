using System;
using System.Collections;
using UnityEngine;

public class Trampoline : MonoBehaviour, IObstacle
{
    public float jumpingPower = 15f;
    private Animator _animator;

    private void Start() {
        _animator = GetComponent<Animator>();
    }

    public void InteractWithObstacle(GameObject player) {
        OnCollisionEnter2D();
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, jumpingPower);
    }

    private void OnCollisionEnter2D() {
        _animator.SetBool("IsJumping", true);
        StartCoroutine(SetBackToIdle());
    }

    private IEnumerator SetBackToIdle() {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        _animator.SetBool("IsJumping", false);
    }
}
