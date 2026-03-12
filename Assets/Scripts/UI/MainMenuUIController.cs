using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUIController : MonoBehaviour
{
    private VisualElement root;
    private Button startButton;
    private Button optionsButton;
    private Button exitButton;

    public static Action OnStartGame;
    public static Action OnOptions;
    public static Action OnExitGame;


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
        OnStartGame?.Invoke();
    }
    private void OptionsButton_clicked()
    {
        OnOptions?.Invoke();
        Debug.Log("Options are not implemented yet.");
    }

    private void ExitButton_clicked()
    {
        OnExitGame?.Invoke();
    }
}
