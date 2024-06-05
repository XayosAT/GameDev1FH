using System;
using System.Collections;
using UnityEngine;

public class Trampoline : MonoBehaviour, IObstacle
{
    public float jumpingPower = 15f;
    private Animator _animator;
    private float _jumping;

    private void Start() {
        _animator = GetComponent<Animator>();
        UpdateAnimClipTimes();
    }

    private void UpdateAnimClipTimes() {
        AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips) {
            _jumping = clip.name switch {
                "Jump" => clip.length,
                _ => _jumping
            };
        }
    }

    public void InteractWithObstacle(GameObject player) {
        _animator.SetBool("IsJumping", true);
        StartCoroutine(SetBackToIdle());
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, jumpingPower);
    }
    private IEnumerator SetBackToIdle() {
        yield return new WaitForSeconds(_jumping);
        _animator.SetBool("IsJumping", false);
    }
}
