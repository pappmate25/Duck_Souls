using UnityEngine;

public class Weaponchoice : MonoBehaviour, IInteractable
{
    [SerializeField] private GameEventSO Weaponchoice_onChooseThis;
    [SerializeField] private GameEventSO Weaponchoice_onChooseOther;
    public void Interact()
    {
        Weaponchoice_onChooseThis.Invoke();
        Disable();
    }

    private void OnEnable()
    {
        Weaponchoice_onChooseOther.Subscribe(Disable);
    }

    private void OnDisable()
    {
        Weaponchoice_onChooseOther.UnSubscribe(Disable);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
