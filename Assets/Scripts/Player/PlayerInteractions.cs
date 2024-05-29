using System;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Detection if something hit player
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            Vector2 contactPoint = other.GetContact(0).point;
            Vector2 center = other.collider.bounds.center;
            
            IKillableEnemy killableEnemy = other.gameObject.GetComponent<IKillableEnemy>();
            if (killableEnemy != null) {
                GetComponentInParent<PlayerStats>().AddEnemyKilled();
                killableEnemy.InteractWithEnemy(gameObject, other);
            }
        }
    }
}
