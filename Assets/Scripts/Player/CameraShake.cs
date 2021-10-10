using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{

    [SerializeField] private float amplitude;
    [SerializeField] private float baseShake;
    [SerializeField] private CinemachineVirtualCamera virtualCam;
    private CinemachineBasicMultiChannelPerlin noise;

    // Start is called before the first frame update
    void Start()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
        noise = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        virtualCam.Follow = Player.Instance.transform;
    }

    public void SetShake(bool enabled)
    {
        if(enabled)
        {
            noise.m_AmplitudeGain = amplitude;
        } else
        {
            noise.m_AmplitudeGain = 0;
        }
    }

    public void SetShakeLevel(int lvl)
    {
        amplitude = lvl * baseShake;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
