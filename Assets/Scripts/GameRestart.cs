using UnityEngine;
using UnityEngine.UI;

public class GameRestart : MonoBehaviour
{
    private Button button;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        button.onClick.AddListener(ButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ButtonClicked()
    {
        _gameManager.ResetGame();
    }
}
