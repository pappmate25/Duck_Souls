using System;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] GameEventSO NPC_onInteraction;

    public void DungeonInteract()
    {
        Debug.Log("Talking with dungeon guardian");
        NPC_onInteraction.Invoke();
    }
}
