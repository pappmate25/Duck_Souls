using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineCamera cmCam;
    private CinemachineBasicMultiChannelPerlin noise;
    private float intensity = 3f;
    private float duration = 0.2f;


    private void Awake()
    {
        cmCam = GetComponent<CinemachineCamera>();
        noise = cmCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
    }
    
    public void Shake()
    {
        StartCoroutine(ShakeRoutine());
    }

    private IEnumerator ShakeRoutine()
    {
        noise.AmplitudeGain = intensity;

        yield return new WaitForSeconds(duration);

        noise.AmplitudeGain = 0;
    }
}
