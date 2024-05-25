using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour, IObstacle
{
    // Start is called before the first frame update
    public void InteractWithObstacle(GameObject player)
    {
        Debug.Log("Player hit a spike");
    }
}
