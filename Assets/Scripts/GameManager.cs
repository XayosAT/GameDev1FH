using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Static variable that holds the instance
    //private static GameManager _instance;
    private Dictionary<TeamColor, int> _teamScores = new Dictionary<TeamColor, int>();

    public GameObject titleScreen;
    public GameObject startButton;
    public GameObject countDownScreen;
    public GameObject winScreen;
    public List<GameObject> singlePlayerObjects;
    public List<GameObject> multiPlayerObjects;
    public int neededWinCoins = 3;
    public GameObject textRedTeam;
    public GameObject textBlueTeam;

    private float _countdown = 5;
    private bool isMultiplayer = false;
    private bool _gameIsRunning = true;

    // Property to access the instance
    /*public static GameManager Instance
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
    }*/

    // Ensure that the instance is not destroyed between scenes
    private void Awake()
    {
        /*if (_instance != null && _instance != this)
        {
            Destroy(gameObject); // Ensure there are no duplicate instances
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy this object when loading new scenes
        }*/

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
    }

    private void WinCheck()
    {
        if (_gameIsRunning && isMultiplayer)
        {
            if (_teamScores[TeamColor.Red] >= neededWinCoins)
            {
                textBlueTeam.gameObject.SetActive(false);
                textRedTeam.gameObject.SetActive(true);
                ShowWinScreens();
            }
            else if (_teamScores[TeamColor.Blue] >= neededWinCoins)
            {
                textBlueTeam.gameObject.SetActive(true);
                textRedTeam.gameObject.SetActive(false);
                ShowWinScreens();
            }
        }
        else if (_gameIsRunning)
        {
            if (_teamScores[TeamColor.Red] >= neededWinCoins ||
                _teamScores[TeamColor.Blue] >= neededWinCoins)
            {
                ShowWinScreens();
            }
        }
    }

    private void ShowWinScreens()
    {
        _gameIsRunning = false;
        if (isMultiplayer)
        {
            GameObject.Find("P1 Cam - Main").GetComponent<BackgroundMusicHandling>().StopMusic();
        }
        else
        {
            GameObject.Find("Main Camera").GetComponent<BackgroundMusicHandling>().StopMusic();
        }
        winScreen.gameObject.SetActive(true);
        winScreen.gameObject.GetComponent<AudioSource>().Play();
        StartCoroutine(GameStop(2f));
    }

    private IEnumerator GameStop(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        titleScreen.gameObject.SetActive(false);
        countDownScreen.gameObject.SetActive(true);
        StartCoroutine(Countdown(1f));
    }

    private IEnumerator Countdown(float seconds)
    {
        while (_countdown >= 1)
        {
            countDownScreen.GetComponent<TextMeshProUGUI>().text = _countdown.ToString();
            _countdown -= 1;
            yield return new WaitForSecondsRealtime(seconds);
        }
        Time.timeScale = 1.0f;
        countDownScreen.gameObject.SetActive(false);
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
        isMultiplayer = true;
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
        isMultiplayer = false;
    }

    public void ShowStartButton()
    {
        startButton.gameObject.SetActive(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

