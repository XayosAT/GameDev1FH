using System;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    private AudioSource _audioSource;
    public int coinValue;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _audioSource.Play();
            other.GetComponent<PlayerStats>().CoinCollected(2);
            Destroy(gameObject, 0.1f);
        }
    }
}
