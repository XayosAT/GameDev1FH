using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : MonoBehaviour
{
    public GameObject bulletPrefab; // The bullet prefab
    private Vector2 _bulletSpawnPoint; // The point where bullets are spawned
    public float shootInterval = 1f; // Time between shots

    private float _shootTimer;
    void Start()
    {
        //bulletspawnpoint is the position of the trunk a little to the left
        _bulletSpawnPoint = new Vector2(transform.position.x - 0.3f, transform.position.y -0.1f);
        
    }
    void Update()
    {
        _shootTimer += Time.deltaTime;
        if (_shootTimer >= shootInterval)
        {
            Shoot();
            _shootTimer = 0f;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, _bulletSpawnPoint, Quaternion.identity);
    }
}
