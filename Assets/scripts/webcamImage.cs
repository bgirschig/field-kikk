using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class webcamImage : MonoBehaviour
{
    string cameraName;
    WebCamTexture webcam;
    Image targetImage;
    AspectRatioFitter fitter;

    // Start is called before the first frame update
    void Start()
    {
        targetImage = GetComponent<Image>();

        var devices = WebCamTexture.devices;
        cameraName = WebCamTexture.devices[0].name;

        webcam = new WebCamTexture(cameraName);
        var material = Instantiate(targetImage.material);
        material.mainTexture = webcam;
        targetImage.material = material;
        
        fitter = GetComponent<AspectRatioFitter>();
        webcam.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (webcam.width > 100) {
            fitter.aspectRatio = (float)webcam.width / webcam.height;
        }
    }

    void OnDisable() {
        webcam.Stop();
    }
    void OnEnable() {
        webcam.Play();
    }
}
