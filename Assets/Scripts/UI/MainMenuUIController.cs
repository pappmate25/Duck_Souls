using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private GameEventSO MainMenuUIController_onStartGame;
    [SerializeField] private GameEventSO MainMenuUIController_onOptions;
    [SerializeField] private GameEventSO MainMenuUIController_onExitGame;

    private VisualElement root;
    private Button startButton;
    private Button optionsButton;
    private Button exitButton;


    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("start-game-button");
        optionsButton = root.Q<Button>("options-button");
        exitButton = root.Q<Button>("exit-game-button");
    }

    private void OnEnable()
    {
        startButton.clicked += StartButton_clicked;
        optionsButton.clicked += OptionsButton_clicked;
        exitButton.clicked += ExitButton_clicked;
    }

    private void OnDisable()
    {
        startButton.clicked -= StartButton_clicked;
        optionsButton.clicked -= OptionsButton_clicked;
        exitButton.clicked -= ExitButton_clicked;
    }

    private void StartButton_clicked()
    {
        MainMenuUIController_onStartGame.Invoke();
    }
    private void OptionsButton_clicked()
    {
        MainMenuUIController_onOptions.Invoke();
        Debug.Log("Options are not implemented yet.");
    }

    private void ExitButton_clicked()
    {
        MainMenuUIController_onExitGame.Invoke();
    }
}
