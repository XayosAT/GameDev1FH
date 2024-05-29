using Unity.VisualScripting;
using UnityEngine;

public class PlayerFootInteraction : MonoBehaviour {
    private PlayerAudioHandler _audioHandler;
    private GameObject _player;

    // Start is called before the first frame update
    void Start() {
        _audioHandler = GetComponentInParent<PlayerAudioHandler>();
        _player = gameObject.transform.parent.gameObject;
    }


    //Detection if player jumped on something
    private void OnCollisionEnter2D(Collision2D other) {
        if (!other.gameObject.CompareTag("Detector")) {
            _audioHandler.PlaySound("Hit");
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            IKillableEnemy enemy = other.gameObject.GetComponent<IKillableEnemy>();
            if (enemy != null) {
                gameObject.GetComponentInParent<PlayerStats>().AddEnemyKilled();
                enemy.InteractWithPlayerFoot(_player);
            }

            //Unkillable -> Rockhead, Spikehead, Ghost, spikedturtle
            //Name of Tag of Enemy as case Snail, Rockhead, Spikehead, Ghost
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle")) {
            IObstacle obstacle = other.gameObject.GetComponent<IObstacle>();
            if (obstacle != null) {
                obstacle.InteractWithObstacle(_player);
            }
        }
    }
}