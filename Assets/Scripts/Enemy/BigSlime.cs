using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSlime : MonoBehaviour
{
    // OnCollisionEnter2D is called when this collider/rigidbody has begun touching another rigidbody/collider
    private void OnCollisionEnter2D(Collision2D other)
    {
        
        // If the other object is on the ground layer
        //compare tag of other object to player
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            Debug.Log("Player Rb: " + playerRb);
            //add vertical force to player
            playerRb.AddForce(new Vector2(0, 30), ForceMode2D.Impulse);
        }
    }
    
}
