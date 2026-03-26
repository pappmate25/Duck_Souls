using System;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private GameEventSO ExitDoor_onReturnToHub;
    [SerializeField] private GameEventSO RoomClearedEvent;
    [SerializeField] private SpriteRenderer doorVisual;
    [SerializeField] private Color lockedColor = Color.red;
    [SerializeField] private Color unlockedColor = Color.green;

    private bool isUnlocked;


    private void OnEnable()
    {
        isUnlocked = false;
        UpdateVisual();
        RoomClearedEvent.Subscribe(Unlock);
    }

    private void OnDisable()
    {
        RoomClearedEvent.UnSubscribe(Unlock);
    }

    private void Unlock()
    {
        isUnlocked = true;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if(doorVisual != null)
        {
            doorVisual.color = isUnlocked ? lockedColor : unlockedColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isUnlocked) return;

        int layerIndex = LayerMask.NameToLayer("Player");

        if(other.gameObject.layer == layerIndex)
        {
            ExitDoor_onReturnToHub.Invoke();
        }
    }
}
