using System;
using System.Collections;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour {

    private Animator _playerAnim;
    private PlayerMovement _playerMovement;
    private IKillableEnemy _killableEnemy;
    private Rigidbody2D _playerRb;

    void Start() {
        _playerAnim = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerRb = GetComponent<Rigidbody2D>();
    }



    //Detection if something hit player
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            _killableEnemy =  other.gameObject.GetComponent<IKillableEnemy>();
            if (_killableEnemy is { IsHit: false }) {
                _playerMovement.TakeDamage();
                _killableEnemy.InteractWithPlayer(_playerRb, other);
            }

        }
    }

    /*private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            _playerMovement.TakeDamage();
            _killableEnemy =  other.gameObject.GetComponent<IKillableEnemy>();
            if (_killableEnemy is { IsHit: false }) {
                _killableEnemy.InteractWithPlayer(_playerRb, other);
            }
        }
    }*/
}