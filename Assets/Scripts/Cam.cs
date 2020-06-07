using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cam : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    CinemachineBasicMultiChannelPerlin noise;

    public float amplitude = 3f;
    public float duration = 0.2f;

    private void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        Spawner.instance.onLineClear += Shake;
    }

    void Shake()
    {
        noise.m_AmplitudeGain = amplitude;
        Invoke("StopShake", duration);
    }

    void StopShake()
    {
        noise.m_AmplitudeGain = 0;
    }
}
