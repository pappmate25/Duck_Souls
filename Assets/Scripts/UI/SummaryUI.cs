using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SummaryUI : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    [Header("Events")]
    [SerializeField] private GameEventSO PlayerDeathEvent;
    [SerializeField] private GameEventSO EnemyDeathEvent;
    [SerializeField] private GameEventSO SummaryUI_onReturnToHub;
    [SerializeField] private GameEventSO SummaryUI_onOpenSummary;
    [SerializeField] private GameEventSO ExitDoor_onReturnToHub;

    private VisualElement root;
    private Label killCountLabel;
    private Label runResultLabel;
    private Label continueLabel;
    private float pulseDirection = -1f;
    private float opacity = 1f;
    private bool isActive = false;
    private int enemiesKilled;

    //input
    private PlayerInput playerInput;
    private PlayerController playerController;

    private InputAction closeSummaryAction;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        killCountLabel = root.Q<Label>("kill-count-label");
        runResultLabel = root.Q<Label>("run-result-label");
        continueLabel = root.Q<Label>("continue-label");
        root.style.display = DisplayStyle.None;

        playerController = FindFirstObjectByType<PlayerController>();
        playerInput = playerController.GetComponent<PlayerInput>();

        closeSummaryAction = playerInput.actions["CloseSummary"];
    }

    private void OnEnable()
    {
        PlayerDeathEvent.Subscribe(OnDeathSummary);
        ExitDoor_onReturnToHub.Subscribe(OnDungeonCompletionSummary);

        EnemyDeathEvent.Subscribe(OnEnemyKilled);

        closeSummaryAction.performed += CloseSummary;
    }

    private void OnDisable()
    {
        PlayerDeathEvent.UnSubscribe(OnDeathSummary);
        ExitDoor_onReturnToHub.UnSubscribe(OnDungeonCompletionSummary);

        EnemyDeathEvent.UnSubscribe(OnEnemyKilled);

        closeSummaryAction.performed -= CloseSummary;
    }

    private void Update()
    {
        if (isActive)
        {
            PulseLabel();
        }
    }

    private void OnEnemyKilled()
    {
        enemiesKilled++;
    }

    private void OnDeathSummary()
    {
        runResultLabel.text = $"You Died";
        ShowSummary();
    }

    private void OnDungeonCompletionSummary()
    {
        runResultLabel.text = $"Dungeon Completed";
        ShowSummary();
    }

    private void ShowSummary()
    {
        isActive = true;

        if (killCountLabel != null)
        {
            killCountLabel.text = $"Enemies Killed: {enemiesKilled}";
        }

        SummaryUI_onOpenSummary.Invoke();

        uiManager.OpenUI(root, true);
    }

    private void CloseSummary(InputAction.CallbackContext context)
    {
        isActive = false;
        enemiesKilled = 0;

        uiManager.CloseTopUI(true);
        SummaryUI_onReturnToHub.Invoke();
    }

    private void PulseLabel()
    {
        opacity += pulseDirection * Time.unscaledDeltaTime * 0.5f;

        if (opacity <= 0f)
        {
            opacity = 0f;
            pulseDirection = 1f;
        }
        else if (opacity >= 1f)
        {
            opacity = 1f;
            pulseDirection = -1f;
        }

        continueLabel.style.opacity = opacity;
    }
}
