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
    [SerializeField] private GameEventSO DeathSummaryUI_onReturnToHub;
    [SerializeField] private GameEventSO DeathSummaryUI_onOpenSummary;

    private PlayerInput playerInput;
    private InputActionMap previousActionMap;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        Debug.Log(gameObject.name + " " + playerInput.currentActionMap.name);
    }

    private void OnEnable()
    {
        UIController_onPauseGame.Subscribe(SetToUI);
        UIManager_onPauseClosed.Subscribe(SetToDefault);

        DeathSummaryUI_onOpenSummary.Subscribe(SetToSummary);
        DeathSummaryUI_onReturnToHub.Subscribe(SetToDefault);
    }

    private void OnDisable()
    {
        UIController_onPauseGame.UnSubscribe(SetToUI);
        UIManager_onPauseClosed.UnSubscribe(SetToDefault);

        DeathSummaryUI_onOpenSummary.UnSubscribe(SetToSummary);
        DeathSummaryUI_onReturnToHub.UnSubscribe(SetToDefault);
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
