using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakes = 0f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    bool cameraShaking;

    void Start()
    {
        originalPos = gameObject.transform.position;
        cameraShaking = false;
    }

    void FixedUpdate()
    {
        if (cameraShaking)
        {
            if (shakes > 0)
            {
                gameObject.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
                gameObject.transform.position += new Vector3(0f, 0f, -50f);
                shakes -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakes = 0f;
                gameObject.transform.localPosition = originalPos;
                cameraShaking = false;
            }
        }
    }

    public void ShakeCamera(float shaking)
    {
        shakes = shaking;
        originalPos = gameObject.transform.position;
        cameraShaking = true;
    }
}