using UnityEngine;
using UnityEngine.UI;

public class SelectGameMode : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;

    public bool isMultiplayer;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        button.onClick.AddListener(ButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ButtonClicked()
    {
        if (isMultiplayer)
        {
            gameManager.ShowMultiplayer();
        }
        else
        {
            gameManager.ShowSingleplayer();
        }
    }
}
