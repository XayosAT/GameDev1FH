using UnityEngine;

public class PlayerFootInteraction : MonoBehaviour
{
    private PlayerAudioHandler _audioHandler;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioHandler = GetComponentInParent<PlayerAudioHandler>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Detection if player jumped on something
    private void OnTriggerEnter2D(Collider2D other)
    {
        _audioHandler.PlaySound("Hit");
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //Name of Tag of Enemy as case
            switch (other.gameObject.tag)
            {
                case "Slime":
                    gameObject.GetComponentInParent<PlayerStats>().AddEnemyKilled();
                    other.GetComponent<EnemyHorizontalController>().PlayerKill();
                    break;
                case "TurtleSpike":

                    break;
                case "Snail":
                    gameObject.GetComponentInParent<PlayerStats>().AddEnemyKilled();
                    other.GetComponent<EnemyHorizontalController>().PlayerKill();
                    break;
                case "Turtle":
                    gameObject.GetComponentInParent<PlayerStats>().AddEnemyKilled();
                    other.GetComponent<EnemyHorizontalController>().PlayerKill();
                    break;
                case "Bat":
                    gameObject.GetComponentInParent<PlayerStats>().AddEnemyKilled();
                    other.GetComponent<EnemyHorizontalController>().PlayerKill();
                    break;
                case "BlueBird":
                    gameObject.GetComponentInParent<PlayerStats>().AddEnemyKilled();
                    other.GetComponent<EnemyHorizontalController>().PlayerKill();
                    break;
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            switch (other.gameObject.tag)
            {
                case "Trampoline":
                    other.GetComponent<Trampoline>().PlayerJump(GetComponentInParent<Rigidbody2D>());
                    break;
            }
        }
    }
}