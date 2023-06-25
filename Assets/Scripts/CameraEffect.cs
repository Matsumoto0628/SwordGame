using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    [SerializeField]
    float shakeDuration;

    [SerializeField]
    float shakeStrength;

    [SerializeField]
    float shakeVibrato;

    ShakeByPerlinNoise shake;

    Camera cam;

    Color defaultCol;

    private void Start()
    {
        shake = GetComponent<ShakeByPerlinNoise>();

        cam = Camera.main;

        defaultCol = cam.backgroundColor;
    }

    public void ShakeCamera()
    {
        shake.StartShake(shakeDuration, shakeStrength, shakeVibrato);

        Invoke("DefaultCol", 0.5f);
    }

    public void BackRed()
    {
        cam.backgroundColor = Color.red;
    }

    private void DefaultCol()
    {
        cam.backgroundColor = defaultCol;
    }
}
