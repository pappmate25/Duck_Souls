using UnityEngine;
using UnityEngine.InputSystem;

public enum ActionMaps
{
    Hub,
    Dungeon,
    UI,
    Summary
}

public class ActionMapController : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private GameEventSO UIController_onPauseGame;
    [SerializeField] private GameEventSO UIManager_onPauseClosed;
    [SerializeField] private GameEventSO SummaryUI_onReturnToHub;
    [SerializeField] private GameEventSO SummaryUI_onOpenSummary;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    //private void Update()
    //{
    //    Debug.Log(gameObject.name + " " + playerInput.currentActionMap.name);
    //}

    private void OnEnable()
    {
        UIController_onPauseGame.Subscribe(SetToUI);
        UIManager_onPauseClosed.Subscribe(SetToDefault);

        SummaryUI_onOpenSummary.Subscribe(SetToSummary);
        SummaryUI_onReturnToHub.Subscribe(SetToDefault);
    }

    private void OnDisable()
    {
        UIController_onPauseGame.UnSubscribe(SetToUI);
        UIManager_onPauseClosed.UnSubscribe(SetToDefault);

        SummaryUI_onOpenSummary.UnSubscribe(SetToSummary);
        SummaryUI_onReturnToHub.UnSubscribe(SetToDefault);
    }

    private void SetToUI()
    {
        playerInput.SwitchCurrentActionMap(ActionMaps.UI.ToString());
    }

    private void SetToSummary()
    {
        playerInput.SwitchCurrentActionMap(ActionMaps.Summary.ToString());
    }
    private void SetToDefault()
    {
        playerInput.SwitchCurrentActionMap(playerInput.defaultActionMap);
    }

    //private void SetToHub()
    //{
    //    playerInput.SwitchCurrentActionMap(ActionMaps.Hub.ToString());
    //}
}
