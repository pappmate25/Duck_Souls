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
        //UIController.OnPauseGame += SetToUI;
        UIManager.OnPauseClosed += SetToPrevious;
    }

    private void OnDisable()
    {
        //UIController.OnPauseGame -= SetToUI;
        UIManager.OnPauseClosed -= SetToPrevious;
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
