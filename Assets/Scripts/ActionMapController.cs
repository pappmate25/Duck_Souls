using UnityEngine;
using UnityEngine.InputSystem;

public enum ActionMaps
{
    Hub,
    Dungeon,
    UI
}

public class ActionMapController : MonoBehaviour
{
    [SerializeField] private GameEventSO onPauseGame;
    [SerializeField] private GameEventSO onPauseClosed;

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
        onPauseGame.Subscribe(SetToUI);
        onPauseGame.Subscribe(SetToPrevious);
    }

    private void OnDisable()
    {
        onPauseGame.UnSubscribe(SetToUI);
        onPauseClosed.UnSubscribe(SetToPrevious);
    }

    private void SetToUI()
    {
        previousActionMap = playerInput.currentActionMap;
        playerInput.SwitchCurrentActionMap(ActionMaps.UI.ToString());
    }

    private void SetToPrevious()
    {
        if(previousActionMap.name == ActionMaps.Hub.ToString())
        {
            playerInput.SwitchCurrentActionMap(ActionMaps.Hub.ToString());
        }
        else
        {
            playerInput.SwitchCurrentActionMap(ActionMaps.Dungeon.ToString());
        }       
    }
}
