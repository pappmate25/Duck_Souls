using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuController : MonoBehaviour
{
    private VisualElement root;
    private Button continueButton;
    private Button optionsButton;
    private Button leaveDungeonButton;
    private Button exitButton;

    public static Action OnContinueGame;
    public static Action OnPauseOptions;
    public static Action OnLeaveDungeon;
    public static Action OnExitGame;


    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        continueButton = root.Q<Button>("pause-continue-button");
        optionsButton = root.Q<Button>("pause-options-button");
        leaveDungeonButton = root.Q<Button>("pause-leave-dungeon-button");
        exitButton = root.Q<Button>("pause-exit-button");

        root.style.display = DisplayStyle.None;

        //if (!isDungeonActive)
        //{
        //    leaveDungeonButton.style.backgroundColor = new Color(91, 91, 91);
        //    leaveDungeonButton.pickingMode = PickingMode.Ignore;
        //}
        //else
        //{
        //    leaveDungeonButton.style.backgroundColor = new Color(255, 255, 255);
        //    leaveDungeonButton.pickingMode = PickingMode.Position;
        //}
    }

    private void OnEnable()
    {
        continueButton.clicked += ContinueButton_clicked;
        optionsButton.clicked += OptionsButton_clicked;
        leaveDungeonButton.clicked += LeaveDungeonButton_clicked;
        exitButton.clicked += ExitButton_clicked;

        UIController.OnOpenUI += OnPause;
    }

    private void OnDisable()
    {
        continueButton.clicked -= ContinueButton_clicked;
        optionsButton.clicked -= OptionsButton_clicked;
        leaveDungeonButton.clicked -= LeaveDungeonButton_clicked;
        exitButton.clicked -= ExitButton_clicked;

        UIController.OnOpenUI -= OnPause;
    }

    private void ContinueButton_clicked()
    {
        OnContinueGame?.Invoke();
    }

    private void OptionsButton_clicked()
    {
        OnPauseOptions?.Invoke();
    }

    private void LeaveDungeonButton_clicked()
    {
        OnLeaveDungeon?.Invoke();
    }

    private void ExitButton_clicked()
    {
        OnExitGame?.Invoke();
    }

    private void OnPause(bool isPaused)
    {
        root.style.display = isPaused ? DisplayStyle.Flex : DisplayStyle.None;
    }
}
