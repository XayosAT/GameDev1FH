using Unity.VisualScripting;
using UnityEngine;

public class PlayerFootInteraction : MonoBehaviour
{
    private PlayerAudioHandler _audioHandler;
    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _audioHandler = GetComponentInParent<PlayerAudioHandler>();
        _player = gameObject.transform.parent.gameObject;
    }

    

    //Detection if player jumped on something
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Detector"))
        {
            _audioHandler.PlaySound("Hit");
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            IKillableEnemy enemy = other.gameObject.GetComponent<IKillableEnemy>();
            if (enemy != null) {
                //gameObject.GetComponentInParent<PlayerStats>().AddEnemyKilled();
                //enemy.InteractWithEnemy(_player);
            }
            
            //Name of Tag of Enemy as case
            switch (other.gameObject.tag)
            {
                /*case "Slime":
                    gameObject.GetComponentInParent<PlayerStats>().AddEnemyKilled();
                    other.GetComponent<EnemyHorizontalController>().PlayerKill();
                    break;*/
                /*case "TurtleSpike":

                    break;*/
                case "Snail":
                    gameObject.GetComponentInParent<PlayerStats>().AddEnemyKilled();
                    other.GetComponent<EnemyHorizontalController>().PlayerKill();
                    break;
                /*case "Turtle":
                    gameObject.GetComponentInParent<PlayerStats>().AddEnemyKilled();
                    other.GetComponent<EnemyHorizontalController>().PlayerKill();
                    break;*/
                /*case "Bat":
                    gameObject.GetComponentInParent<PlayerStats>().AddEnemyKilled();
                    other.GetComponent<EnemyHorizontalController>().PlayerKill();
                    break;*/
                /*case "BlueBird":
                    gameObject.GetComponentInParent<PlayerStats>().AddEnemyKilled();
                    other.GetComponent<EnemyHorizontalController>().PlayerKill();
                    break;*/
                case "Ghost":
                    gameObject.GetComponentInParent<PlayerStats>().AddEnemyKilled();
                    other.GetComponent<EnemyHorizontalController>().PlayerKill();
                    break;
                case "RockHead":

                    break;
                case "SpikeHead":

                    break;
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            // switch (other.gameObject.tag)
            // {
            //     case "Trampoline":
            //         other.GetComponent<Trampoline>().PlayerJump(GetComponentInParent<Rigidbody2D>());
            //         break;
            // }
            IObstacle obstacle = other.gameObject.GetComponent<IObstacle>();
            if (obstacle != null)
            {
                obstacle.InteractWithObstacle(_player);
            }
        }
    }
}