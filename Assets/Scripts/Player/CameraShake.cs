using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineCamera cmCam;
    private CinemachineBasicMultiChannelPerlin noise;
    private float intensity = 3f;
    private float duration = 0.2f;

    private Coroutine shakeCoroutine;

    private void Awake()
    {
        cmCam = GetComponent<CinemachineCamera>();
        noise = cmCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void OnEnable()
    {
        DamageDealer.OnPlayerHit += Shake;
    }

    private void OnDisable()
    {
        DamageDealer.OnPlayerHit -= Shake;
    }

    public void Shake()
    {
        if(shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
        }

        shakeCoroutine = StartCoroutine(ShakeRoutine());
    }

    private IEnumerator ShakeRoutine()
    {
        noise.AmplitudeGain = intensity;

        yield return new WaitForSeconds(duration);

        noise.AmplitudeGain = 0;

        shakeCoroutine = null;
    }
}
