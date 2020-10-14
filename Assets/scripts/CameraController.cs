using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public DetectorClient detectorClient;
    public float speedForward = 0.02f;
    public float speedBackward = 0.01f;

    private float currentVelocity = 0;
    private float target = 0;
    private float current = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.eulerAngles.x;
        current = transform.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (detectorClient.speed > 0) target += detectorClient.speed * speedBackward;
        else target += detectorClient.speed * speedForward;

        current = Mathf.SmoothDampAngle(current, target, ref currentVelocity, 0.3f);
        transform.rotation = Quaternion.Euler(current, 0, 0);
    }
}
