using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    public static Action<bool> OnOpenUI;

    private PlayerInput playerInput;
    private InputActionMap currentMap;
    private InputAction pauseAction;
    private bool isPaused;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        pauseAction = playerInput.actions["Pause"];
    }


    private void OnEnable()
    {
        pauseAction.performed += TogglePause;

        PauseMenuController.OnContinueGame += Resume;
    }

    private void OnDisable()
    {
        pauseAction.performed -= TogglePause;

        PauseMenuController.OnContinueGame -= Resume;
    }

    private void TogglePause(InputAction.CallbackContext context)
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    private void Pause()
    {
        isPaused = true;

        Time.timeScale = 0f;

        OnOpenUI?.Invoke(true);

        currentMap = playerInput.currentActionMap;
        playerInput.SwitchCurrentActionMap("UI");
    }

    private void Resume()
    {
        isPaused = false;

        Time.timeScale = 1f;

        OnOpenUI?.Invoke(false);

        playerInput.SwitchCurrentActionMap(currentMap.name);
    }
}
