using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour
{
    Canvas ui;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        Screen.fullScreen = true;

        ui = GameObject.FindObjectOfType<Canvas>();
        ui.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m")) ui.enabled = !ui.enabled;
        if (Input.GetKeyDown("f")) Screen.fullScreen = !Screen.fullScreen;
    }
}
