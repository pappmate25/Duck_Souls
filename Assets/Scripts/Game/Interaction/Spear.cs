using Unity.VisualScripting;
using UnityEngine;

public class Spear : MonoBehaviour, IInteractable
{
    [SerializeField] private GameEventSO Spear_onSpearChoose;
    [SerializeField] private GameEventSO Sword_onSwordChoose;
    public void Interact()
    {
        Spear_onSpearChoose.Invoke();
        Disable();
    }

    private void OnEnable()
    {
        Sword_onSwordChoose.Subscribe(Disable);
    }

    private void OnDisable()
    {
        Sword_onSwordChoose.UnSubscribe(Disable);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
