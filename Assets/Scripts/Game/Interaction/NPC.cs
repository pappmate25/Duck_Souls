using System;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public static Action OnInteraction;
    public void DungeonInteract()
    {
        Debug.Log("Talking with dungeon guardian");
        OnInteraction?.Invoke();
    }
}
