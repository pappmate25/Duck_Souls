using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable currentInteractable;

    public static Action<bool> OnShowInteractUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponentInParent<IInteractable>();

        if (interactable != null)
        {
            currentInteractable = interactable;
            Debug.Log("uuu egy NPC");
            OnShowInteractUI?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if( other.GetComponentInParent<IInteractable>() == currentInteractable)
        {
            currentInteractable = null;
            Debug.Log("Viszlát NPC");
            OnShowInteractUI?.Invoke(false);
        }
    }

    public void Interact()
    {
        currentInteractable?.DungeonInteract();
    }
}
