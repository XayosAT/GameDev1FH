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
        _animator.SetBool("IsJumping", true);
        StartCoroutine(SetBackToIdle());
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, jumpingPower);
    }
    private IEnumerator SetBackToIdle() {
        yield return new WaitForSeconds(1f);
        _animator.SetBool("IsJumping", false);
    }
}
