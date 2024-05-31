using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controllers : MonoBehaviour
{
    private TextMeshProUGUI controllers;

    // Start is called before the first frame update
    void Start()
    {
        controllers = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        controllers.text = "-";
        foreach (var device in InputSystem.devices)
            controllers.text += device.name;
    }
}
