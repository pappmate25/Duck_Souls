using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private GameEventSO MainMenuUIController_onStartGame;
    [SerializeField] private GameEventSO MainMenuUIController_onContinueGame;
    [SerializeField] private GameEventSO MainMenuUIController_onExitGame;

    private VisualElement root;
    private Button startButton;
    private Button continueGameButton;
    private Button exitButton;


    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("start-game-button");
        continueGameButton = root.Q<Button>("continue-game-button");
        exitButton = root.Q<Button>("exit-game-button");
    }

    private void OnEnable()
    {
        startButton.clicked += StartButton_clicked;
        continueGameButton.clicked += ContinueButton_clicked;
        exitButton.clicked += ExitButton_clicked;
    }

    private void OnDisable()
    {
        startButton.clicked -= StartButton_clicked;
        continueGameButton.clicked -= ContinueButton_clicked;
        exitButton.clicked -= ExitButton_clicked;
    }

    private void StartButton_clicked()
    {
        MainMenuUIController_onStartGame.Invoke();
    }
    private void ContinueButton_clicked()
    {
        MainMenuUIController_onContinueGame.Invoke();
    }

    private void ExitButton_clicked()
    {
        MainMenuUIController_onExitGame.Invoke();
    }
}
