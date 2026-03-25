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
    [SerializeField] private GameEventSO UIController_onPauseGame;
    [SerializeField] private GameEventSO UIManager_onPauseClosed;

    private PlayerInput playerInput;
    private InputActionMap previousActionMap;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        Debug.Log(ActionMaps.Hub.ToString());
    }

    //private void Update()
    //{
    //    Debug.Log(gameObject.name + " " + playerInput.currentActionMap.name);
    //}

    private void OnEnable()
    {
        UIController_onPauseGame.Subscribe(SetToUI);
        UIManager_onPauseClosed.Subscribe(SetToPrevious);
    }

    private void OnDisable()
    {
        UIController_onPauseGame.UnSubscribe(SetToUI);
        UIManager_onPauseClosed.UnSubscribe(SetToPrevious);
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
