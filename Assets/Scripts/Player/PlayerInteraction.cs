using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable currentInteractable;

    [SerializeField] private GameEventBoolSO onShowInteractUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponentInParent<IInteractable>();

        if (interactable != null)
        {
            currentInteractable = interactable;
            Debug.Log("uuu egy NPC");
            onShowInteractUI.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponentInParent<IInteractable>() == currentInteractable && currentInteractable != null)
        {
            currentInteractable = null;
            Debug.Log("Viszlát NPC");
            onShowInteractUI.Invoke(false);
        }
    }

    public void Interact()
    {
        currentInteractable?.DungeonInteract();
    }
}
