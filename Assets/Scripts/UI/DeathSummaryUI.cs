using UnityEngine;
using UnityEngine.UIElements;

public class DeathSummaryUI : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    [Header("Events")]
    [SerializeField] private GameEventSO PlayerDeathEvent;
    [SerializeField] private GameEventSO EnemyDeathEvent;
    [SerializeField] private GameEventSO ReturnToHubEvent;

    private VisualElement root;
    private Label killCountLabel;
    private int enemiesKilled;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        killCountLabel = root.Q<Label>("kill-count-label");
        root.style.display = DisplayStyle.None;
    }

    private void OnEnable()
    {
        PlayerDeathEvent.Subscribe(ShowDeathSummary);
        EnemyDeathEvent.Subscribe(OnEnemyKilled);
    }

    private void OnDisable()
    {
        PlayerDeathEvent.UnSubscribe(ShowDeathSummary);
        EnemyDeathEvent.UnSubscribe(OnEnemyKilled);
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

        uiManager.OpenUI(root, true);
    }

    private void Update()
    {
        if (root.style.display == DisplayStyle.Flex && Input.GetKeyDown(KeyCode.Return))
        {
            root.style.display = DisplayStyle.None;
            Time.timeScale = 1f;
            enemiesKilled = 0;
            ReturnToHubEvent?.Invoke();
        }
    }
}
