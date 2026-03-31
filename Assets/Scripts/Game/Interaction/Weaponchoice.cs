using UnityEngine;

public class Weaponchoice : MonoBehaviour, IInteractable
{
    [SerializeField] private GameEventSO Weaponchoice_onChooseThis;
    [SerializeField] private GameEventSO Weaponchoice_onChooseOther;
    [SerializeField] private GameEventSO RoomManager_onRoomCleared;
    public void Interact()
    {
        Weaponchoice_onChooseThis.Invoke();
        RoomManager_onRoomCleared.Invoke(); //Unlocks the doors in the StartRoom after choosing a weapon
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
