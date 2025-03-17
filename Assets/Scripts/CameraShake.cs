using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    CinemachineBasicMultiChannelPerlin vcam;
    public CinemachineVirtualCamera vcamObj;
    public float amount;
    void Start()
    {
        amount = 0f;
        vcam = vcamObj.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    void Update()
    {
        vcam.m_AmplitudeGain = amount;
    }
}
