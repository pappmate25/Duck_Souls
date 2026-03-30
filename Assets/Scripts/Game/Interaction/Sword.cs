using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour, IInteractable
{
    [SerializeField] private GameEventSO Sword_onSwordChoose;
    [SerializeField] private GameEventSO Spear_onSpearChoose;
    public void Interact()
    {
        Sword_onSwordChoose.Invoke();
        Disable();
    }

    private void OnEnable()
    {
        Spear_onSpearChoose.Subscribe(Disable);
    }

    private void OnDisable()
    {
        Spear_onSpearChoose.UnSubscribe(Disable);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
