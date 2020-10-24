using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetecorCameraImage : MonoBehaviour
{
    float nextFrameTime = 0;

    DetectorClient client;
    Image targetImage;
    AspectRatioFitter fitter;

    // Start is called before the first frame update
    void Start()
    {
        client = GameObject.FindObjectOfType<DetectorClient>();
        targetImage = GetComponent<Image>();
        fitter = GetComponent<AspectRatioFitter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextFrameTime) {
            targetImage.enabled = true;
            client.requestFrame();
            if (client.cameraTexture) {
                if (targetImage.material.mainTexture == null) targetImage.enabled = false;
                var material = Instantiate(targetImage.material);
                material.mainTexture = client.cameraTexture;
                targetImage.material = material;
                fitter.aspectRatio = (float)client.cameraTexture.width / client.cameraTexture.height;
            }
            nextFrameTime = Time.time + 0.1f;
        }
    }
}
