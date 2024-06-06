using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(ButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ButtonClicked()
    {
        Application.Quit();
    }
}
