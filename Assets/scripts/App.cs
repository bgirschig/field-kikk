using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class App : MonoBehaviour
{
    public Canvas ui;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        Application.targetFrameRate = 30;
        Screen.fullScreen = true;

        yield return 0;
        ui = GameObject.FindObjectOfType<Canvas>();
        ui.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m")) ui.gameObject.SetActive(!ui.gameObject.activeInHierarchy);
        if (Input.GetKeyDown("f")) Screen.fullScreen = !Screen.fullScreen;
    }

    // Deliberately crash the app to see if it restarts properly
    public void crash() {
        Debug.Log("Crashing the app");
        Utils.ForceCrash(ForcedCrashCategory.FatalError);
    }
}
