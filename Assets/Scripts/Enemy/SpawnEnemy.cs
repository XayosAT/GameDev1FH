using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Vector3 spawnPosition;
    public float spawnRate = 2f;
    public float startDelay = 0;
    public int numberEnemies = 1;
    public GameObject enemy;
    public LayerMask detectionLayer;

    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        InvokeRepeating("callSpawnCheck", startDelay, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void callSpawnCheck()
    {
        spawnCheck();
    }

    private void spawnCheck()
    {
        var detectedObjects = Physics2D.OverlapAreaAll(new Vector3(boxCollider.bounds.center.x - boxCollider.bounds.extents.x,
            boxCollider.bounds.center.y - boxCollider.bounds.extents.y),
            new Vector3(boxCollider.bounds.center.x + boxCollider.bounds.extents.x,
            boxCollider.bounds.center.y + boxCollider.bounds.extents.y), detectionLayer);
        if (detectedObjects != null && detectedObjects.Length / 2 < numberEnemies)
        {
            spawnEnemy();
        }
    }

    void spawnEnemy()
    {
        Instantiate(enemy, spawnPosition, enemy.transform.rotation);
    }
}
