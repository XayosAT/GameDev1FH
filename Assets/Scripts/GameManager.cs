using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Static variable that holds the instance
    private static GameManager _instance;
    private Dictionary<TeamColor, int> _teamScores = new Dictionary<TeamColor, int>();
    

    // Property to access the instance
    public static GameManager Instance
    {
        get
        {
            // If no instance exists yet, find it or create it
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject gm = new GameObject("GameManager");
                    _instance = gm.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    // Ensure that the instance is not destroyed between scenes
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject); // Ensure there are no duplicate instances
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy this object when loading new scenes
        }
    }
    
    private void Start()
    {
        InitTeamScores();
    }
    
    private void InitTeamScores()
    {
        _teamScores.Add(TeamColor.Red, 0);
        _teamScores.Add(TeamColor.Blue, 0);
    }

    
    public void AddCoinToTeamScore(TeamColor teamColor)
    {
        _teamScores[teamColor]++;
        Debug.Log("Team " + teamColor + " score: " + _teamScores[teamColor]);
    }
    
}

