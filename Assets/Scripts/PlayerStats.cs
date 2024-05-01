using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public GameObject Win;

    private int collectedCoinPoints = 0;
    private int totalCoins = 0;
    private int totalJumped = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CoinCollected(int coinvalue)
    {
        totalCoins++;
        collectedCoinPoints += coinvalue;
        Debug.Log("Total Coins: " + totalCoins);
        Debug.Log("Coin Points: " + collectedCoinPoints);

        if(totalCoins >= 5)
        {
            Win.SetActive(true);
        }
    }

    public void AddJumped()
    {
        totalJumped++;
        Debug.Log("Jumped: " + totalJumped);
    }
}
