using System;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public static Action OnReturnToHub;

    private void OnTriggerEnter2D(Collider2D other)
    {
        int layerIndex = LayerMask.NameToLayer("Player");

        if(other.gameObject.layer == layerIndex)
        {
            OnReturnToHub?.Invoke();
        }
    }
}
