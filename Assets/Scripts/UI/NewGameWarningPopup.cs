using UnityEngine;
using UnityEngine.UIElements;

public class NewGameWarningPopup : MonoBehaviour
{
    [SerializeField] private GameEventSO NewGameWarningPopup_onStartGame;
    [SerializeField] private GameEventSO MainMenuUIController_onStartGame;
    [SerializeField] private UIManager uiManager;

    private VisualElement root;
    private Button cancelButton;
    private Button confirmButton;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        cancelButton = root.Q<Button>("warning-cancel-button");
        confirmButton = root.Q<Button>("warning-confirm-button");

        root.style.display = DisplayStyle.None;
    }

    private void OnEnable()
    {
        MainMenuUIController_onStartGame.Subscribe(ShowUI);

        cancelButton.clicked += CancelButton_clicked;
        confirmButton.clicked += ConfirmButton_clicked;
    }

    private void OnDisable()
    {
        MainMenuUIController_onStartGame.UnSubscribe(ShowUI);

        cancelButton.clicked -= CancelButton_clicked;
        confirmButton.clicked -= ConfirmButton_clicked;
    }

    private void CancelButton_clicked()
    {
        uiManager.CloseTopUI(false);
    }

    private void ConfirmButton_clicked()
    {
        NewGameWarningPopup_onStartGame.Invoke();
    }

    private void ShowUI()
    {
        uiManager.OpenUI(root, false);
    }
}
