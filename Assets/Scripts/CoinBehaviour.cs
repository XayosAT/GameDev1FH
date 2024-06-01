using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    private AudioSource _audioSource;
    public int coinValue;
    private bool _iscollected = false;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !_iscollected)
        {
            _iscollected = true;
            _audioSource.Play();
            //GameManager.Instance.AddCoinToTeamScore(other.GetComponent<PlayerMovement>().teamColor);
            _gameManager.AddCoinToTeamScore(other.GetComponent<PlayerMovement>().teamColor);
            other.GetComponent<PlayerStats>().CoinCollected(coinValue);
            Destroy(gameObject, 0.1f);
        }
    }
}
