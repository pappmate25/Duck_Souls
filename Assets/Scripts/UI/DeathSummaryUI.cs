using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class DeathSummaryUI : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    [Header("Events")]
    [SerializeField] private GameEventSO PlayerDeathEvent;
    [SerializeField] private GameEventSO EnemyDeathEvent;
    [SerializeField] private GameEventSO DeathSummaryUI_onReturnToHub;
    [SerializeField] private GameEventSO DeathSummaryUI_onOpenSummary;

    private VisualElement root;
    private Label killCountLabel;
    private int enemiesKilled;

    //input
    private PlayerInput playerInput;
    private PlayerController playerController;

    private InputAction closeSummaryAction;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        killCountLabel = root.Q<Label>("kill-count-label");
        root.style.display = DisplayStyle.None;

        playerController = FindFirstObjectByType<PlayerController>();
        playerInput = playerController.GetComponent<PlayerInput>();

        closeSummaryAction = playerInput.actions["CloseSummary"];
    }

    private void OnEnable()
    {
        PlayerDeathEvent.Subscribe(ShowDeathSummary);
        EnemyDeathEvent.Subscribe(OnEnemyKilled);

        closeSummaryAction.performed += CloseSummary;
    }

    private void OnDisable()
    {
        PlayerDeathEvent.UnSubscribe(ShowDeathSummary);
        EnemyDeathEvent.UnSubscribe(OnEnemyKilled);

        closeSummaryAction.performed -= CloseSummary;
    }

    private void OnEnemyKilled()
    {
        enemiesKilled++;
    }

    private void ShowDeathSummary()
    {
        if (killCountLabel != null)
        {
            killCountLabel.text = $"Enemies Killed: {enemiesKilled}";
        }

        DeathSummaryUI_onOpenSummary.Invoke();

        uiManager.OpenUI(root, true);
    }

    private void CloseSummary(InputAction.CallbackContext context)
    {
        uiManager.CloseTopUI();
        DeathSummaryUI_onReturnToHub.Invoke();
    }

    //private void Update()
    //{
    //    if (root.style.display == DisplayStyle.Flex && Input.GetKeyDown(KeyCode.Return))
    //    {
    //        root.style.display = DisplayStyle.None;
    //        Time.timeScale = 1f;
    //        enemiesKilled = 0;
    //        ReturnToHubEvent.Invoke();
    //    }
    //}
}
