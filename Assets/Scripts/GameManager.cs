using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Static variable that holds the instance
    private static GameManager _instance;
    private Dictionary<TeamColor, int> _teamScores = new Dictionary<TeamColor, int>();

    public GameObject win;
    public GameObject titleScreen;
    public GameObject startButton;
    public GameObject CountDownScreen;
    public List<GameObject> singlePlayerObjects;
    public List<GameObject> multiPlayerObjects;

    private float _countdown = 5;

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

        Time.timeScale = 0f;
    }

    private void Start()
    {
        InitTeamScores();
    }

    private void Update()
    {
        WinCheck();
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

    private void WinCheck()
    {
        if (_teamScores[TeamColor.Red] >= 3 || _teamScores[TeamColor.Blue] >= 3)
        {
            win.SetActive(true);
        }
    }

    public void StartGame()
    {
        titleScreen.gameObject.SetActive(false);
        CountDownScreen.gameObject.SetActive(true);
        StartCoroutine(Countdown(1f));
    }

    private IEnumerator Countdown(float seconds)
    {
        while (_countdown >= 1)
        {
            CountDownScreen.GetComponent<TextMeshProUGUI>().text = _countdown.ToString();
            _countdown -= 1;
            yield return new WaitForSecondsRealtime(seconds);
        }
        Time.timeScale = 1.0f;
        CountDownScreen.gameObject.SetActive(false);
        _countdown = 5;
    }

    public void ShowMultiplayer()
    {
        foreach (var singlePlayerobject in singlePlayerObjects)
        {
            singlePlayerobject.gameObject.SetActive(false);
        }
        foreach (var multiPlayerobject in multiPlayerObjects)
        {
            multiPlayerobject.gameObject.SetActive(true);
        }
        ShowStartButton();
    }

    public void ShowSingleplayer()
    {
        foreach (var multiPlayerobject in multiPlayerObjects)
        {
            multiPlayerobject.gameObject.SetActive(false);
        }
        foreach (var singlePlayerobject in singlePlayerObjects)
        {
            singlePlayerobject.gameObject.SetActive(true);
        }
        ShowStartButton();
    }

    public void ShowStartButton()
    {
        startButton.gameObject.SetActive(true);
    }
}

