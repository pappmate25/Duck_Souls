using System;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] GameEventSO onInteraction;

    public void DungeonInteract()
    {
        Debug.Log("Talking with dungeon guardian");
        onInteraction?.Invoke();
    }
}
