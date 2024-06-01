using UnityEngine;
using UnityEngine.UI;

public class ShowCredits : MonoBehaviour
{
    private Button button;
    public GameObject credits;

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
        gameObject.transform.parent.gameObject.SetActive(false);
        credits.gameObject.SetActive(true);
    }
}
