using UnityEngine;

public class EnemyHorizontalWallFlip : MonoBehaviour
{
    private EnemyHorizontalController _enemyController;

    // Start is called before the first frame update
    void Start()
    {
        _enemyController = GetComponentInParent<EnemyHorizontalController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") ||
            other.gameObject.layer == LayerMask.NameToLayer("Enemy") ||
            other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            _enemyController.Flip();
        }
    }
}
