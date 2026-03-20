using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    [SerializeField] private GameEventSO onPauseGame;

    private PlayerInput playerInput;
    private InputAction pauseAction;
    private InputAction cancelAction;

    private int lastCancelFrame = -1;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        pauseAction = playerInput.actions["Pause"];
        cancelAction = playerInput.actions["Cancel"];
    }

    private void OnEnable()
    {
        pauseAction.performed += OnCancel;
        cancelAction.performed += OnCancel;
    }

    private void OnDisable()
    {
        pauseAction.performed -= OnCancel;
        cancelAction.performed -= OnCancel;
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        if (lastCancelFrame == Time.frameCount) return;
        lastCancelFrame = Time.frameCount;


        if (uiManager.HasOpenUI())
        {
            uiManager.CloseTopUI();
            return;
        }
        onPauseGame.Invoke();    
    }
}
