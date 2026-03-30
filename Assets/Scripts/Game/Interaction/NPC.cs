using System;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] GameEventSO NPC_onInteraction;

    public void Interact()
    {
        NPC_onInteraction.Invoke();
    }
}
