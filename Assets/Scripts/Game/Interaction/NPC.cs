using System;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] GameEventSO NPC_onInteraction;

    public void DungeonInteract()
    {
        NPC_onInteraction.Invoke();
    }
}
