using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UiController : MonoBehaviour
{
    private DetectorClient detectorClient;
    private Dropdown detectorInputSelector;

    // Start is called before the first frame update
    void Start()
    {
        detectorInputSelector = GameObject.Find("Input Selector").GetComponent<Dropdown>();
        detectorClient = GameObject.FindObjectOfType<DetectorClient>();
        detectorInputSelector.onValueChanged.AddListener(delegate {
            detectorClient.selectInput(detectorInputSelector.value);
        });
    }

    // Update is called once per frame
    void Update()
    {
        updateDropdown(detectorInputSelector, detectorClient.inputOptions);
    }

    void updateDropdown(Dropdown dropdown, List<string> values) {
        if (!matchDropdown(dropdown.options, values)) {
            dropdown.ClearOptions();
            dropdown.AddOptions(values);
        }
    }

    bool matchDropdown(List<Dropdown.OptionData> options, List<string> values) {
        if (options.Count != values.Count) return false;
        for (int i = 0; i < options.Count; i++)
        {
            if (options[i].text != values[i]) return false;
        }
        return true;
    }
}