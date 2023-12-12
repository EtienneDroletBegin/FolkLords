using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camShake : MonoBehaviour
{

    public static camShake instance { get; private set; }

    private CinemachineVirtualCamera cam;
    private float shakeTime;
    private void Start()
    {
        instance = this;
        cam = GetComponent<CinemachineVirtualCamera>();
    }


    public void shake(float intesnity, float timer)
    {
        CinemachineBasicMultiChannelPerlin perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = intesnity;
        shakeTime = timer;
    }

    private void Update()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            if (shakeTime <= 0)
            {
                CinemachineBasicMultiChannelPerlin perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                perlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
