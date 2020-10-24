﻿// Manages the game's options
// linking UI to their actual parameters, saving and loading options

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class OptionsHandler : MonoBehaviour
{
    [Header("Controlled elements")]
    public DetectorClient detectorClient;
    public stars starHandler;
    public CameraController cameraController;

    [Header("Detector config inputs")]
    public Dropdown detectorInput;
    public Toggle flipValues;
    [Header("Scene config inputs")]
    public Slider starCount;
    public Slider starSize;
    public Slider horizon;
    [Header("Interactions config inputs")]
    public Slider autoAdvanceSpeed;
    public Slider forwardSpeed;
    public Slider backwardSpeed;
    [Header("Buttons")]
    public Button saveButton;
    public Button resetButton;

    // Start is called before the first frame update
    void Start() {
        saveButton.interactable = false;
        resetButton.interactable = false;
        init();
    }

    void init() {
        // Input source
        detectorInput.options.Clear();
        detectorInput.AddOptions(detectorClient.inputOptions);

        // Detector config
        initOption("detector.input", detectorInput, val => detectorClient.selectInput(val), "emulator");
        initOption("detector.flip", flipValues, val => detectorClient.flip = val, false);

        // Scene config
        initOption("scene.starCount", starCount, (int val) => starHandler.setStarCount(val), 10000);
        initOption("scene.starSize", starSize, (float val) => starHandler.starSize = val, 1.5f);
        initOption("scene.horizon", horizon, (float val) => Camera.main.transform.localRotation = Quaternion.Euler(val, 0, 0), 0);

        // Interactions config
        initOption("interactions.autoAdvanceSpeed", autoAdvanceSpeed, (float val) => cameraController.autoAdvanceSpeed = val, 0.001f);
        initOption("interactions.forwardSpeed", forwardSpeed, (float val) => cameraController.speedForward = val, 0.02f);
        initOption("interactions.backwardSpeed", backwardSpeed, (float val) => cameraController.speedBackward = val, 0.01f);
    }

    // Initialize a float config option: load from playerprefs, default value, update the global
    // 'resettable' and 'saveable' state, and change callback
    void initOption(string name, Slider input, UnityAction<float> onChange, float defaultValue=0) {
        if (!PlayerPrefs.HasKey(name)) PlayerPrefs.SetFloat(name, defaultValue);
        
        float value = PlayerPrefs.GetFloat(name);
        if (value != defaultValue) resetButton.interactable = true;
        
        input.value = value;
        onChange.Invoke(value);
        
        input.onValueChanged.AddListener(delegate {
            saveButton.interactable = true;
            resetButton.interactable = true;
            onChange.Invoke(input.value);
            PlayerPrefs.SetFloat(name, input.value);
        });
    }

    // Initialize a float config option: load from playerprefs, default value, update the global
    // 'resettable' and 'saveable' state, and change callback
    void initOption(string name, Slider input, UnityAction<int> onChange, int defaultValue=0) {
        if (!PlayerPrefs.HasKey(name)) PlayerPrefs.SetInt(name, defaultValue);
        
        int value = PlayerPrefs.GetInt(name);
        if (value != defaultValue) resetButton.interactable = true;

        input.value = value;
        onChange.Invoke(value);
        
        input.onValueChanged.AddListener(delegate {
            saveButton.interactable = true;
            resetButton.interactable = true;
            onChange.Invoke((int)input.value);
            PlayerPrefs.SetInt(name, (int)input.value);
        });
    }

    // Initialize a string config option: load from playerprefs, default value, update the global
    // 'resettable' and 'saveable' state, and change callback
    void initOption(string name, InputField input, UnityAction<string> onChange, string defaultValue="") {
        if (!PlayerPrefs.HasKey(name)) PlayerPrefs.SetString(name, defaultValue);
        
        string value = PlayerPrefs.GetString(name);
        if (value != defaultValue) resetButton.interactable = true;

        input.text = value;
        onChange.Invoke(value);
        
        input.onEndEdit.AddListener(delegate {
            saveButton.interactable = true;
            resetButton.interactable = true;
            onChange.Invoke(input.text);
            PlayerPrefs.SetString(name, input.text);
        });
    }

    // Initialize a boolean config option: load from playerprefs, default value, update the global
    // 'resettable' and 'saveable' state, and change callback
    void initOption(string name, Toggle input, UnityAction<bool> onChange, bool defaultValue=false) {
        if (!PlayerPrefs.HasKey(name)) PlayerPrefs.SetInt(name, defaultValue ? 1 : 0);
        
        bool value = PlayerPrefs.GetInt(name) == 1;
        if (value != defaultValue) resetButton.interactable = true;

        input.isOn = value;
        onChange.Invoke(value);
        
        input.onValueChanged.AddListener(delegate {
            saveButton.interactable = true;
            resetButton.interactable = true;
            onChange.Invoke(input.isOn);
            PlayerPrefs.SetInt(name, input.isOn ? 1 : 0);
        });
    }

    // Initialize a dropdown config option: load from playerprefs, default value, update the global
    // 'resettable' and 'saveable' state, and change callback
    // Note: dropdown outputs an index, but to keep consistency if the order changes, we are saving
    // the label instead of its index in the options list.
    void initOption(string name, Dropdown input, UnityAction<string> onChange, string defaultValue="emulator") {
        if (!PlayerPrefs.HasKey(name)) PlayerPrefs.SetString(name, defaultValue);
        
        string savedValue = PlayerPrefs.GetString(name);
        if (savedValue != defaultValue) resetButton.interactable = true;

        int valueIndex = input.options.FindIndex(item => { return item.text == savedValue; });
        input.value = valueIndex;
        onChange.Invoke(savedValue);
        
        input.onValueChanged.AddListener(delegate {
            saveButton.interactable = true;
            resetButton.interactable = true;
            string newValue = input.options[input.value].text;
            onChange.Invoke(newValue);
            PlayerPrefs.SetString(name, newValue);
        });
    }

    // reset all options to their defaults
    public void reset() {
        Debug.Log("reset");
        PlayerPrefs.DeleteAll();
        resetButton.interactable = false;
        saveButton.interactable = true;
        init();
    }

    // save the playerprefs
    public void save() {
        PlayerPrefs.Save();
        saveButton.interactable = false;
    }
}
