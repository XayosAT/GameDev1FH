using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    public bool SlimeSpawnCoin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            gameObject.GetComponent<PlayerStats>().AddEnemyKilled();

            //Name of Tag of Enemy as case
            switch (other.gameObject.tag)
            {
                case "Slime":
                    other.GetComponent<SlimeController>().PlayerKill(SlimeSpawnCoin);
                    break;
            }
        }
    }
}
