using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable currentInteractable;

    [SerializeField] private GameEventBoolSO PlayerInteraction_onShowInteractUI;
    [SerializeField] private GameEventBoolSO PlayerInteraction_onShowSpearUI;
    [SerializeField] private GameEventBoolSO PlayerInteraction_onShowSwordUI;

    private int spearLayerIndex;
    private int swordLayerIndex;

    private void Awake()
    {
        spearLayerIndex = LayerMask.NameToLayer("Spear");
        swordLayerIndex = LayerMask.NameToLayer("Sword");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponentInParent<IInteractable>();

        if (interactable != null)
        {
            if (other.gameObject.layer == spearLayerIndex)
            {
                currentInteractable = interactable;
                PlayerInteraction_onShowSpearUI.Invoke(true);
            }
            else if (other.gameObject.layer == swordLayerIndex)
            {
                currentInteractable = interactable;
                PlayerInteraction_onShowSwordUI.Invoke(true);
            }
            else
            {
                //NPC interact
                currentInteractable = interactable;
                PlayerInteraction_onShowInteractUI.Invoke(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponentInParent<IInteractable>();

        if (interactable == currentInteractable && currentInteractable != null)
        {
            if (other.gameObject.layer == spearLayerIndex)
            {
                currentInteractable = null;
                PlayerInteraction_onShowSpearUI.Invoke(false);
            }
            else if (other.gameObject.layer == swordLayerIndex)
            {
                currentInteractable = null;
                PlayerInteraction_onShowSwordUI.Invoke(false);
            }
            else
            {
                //NPC interact
                currentInteractable = null;
                PlayerInteraction_onShowInteractUI.Invoke(false);
            }
        }
    }

    public void Interact()
    {
        currentInteractable?.Interact();
    }
}
