using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuController : MonoBehaviour
{

    [SerializeField] private UIManager uiManager;
    //events
    [SerializeField] private GameEventSO onPauseGame;
    //[SerializeField] private GameEventSO onContinueGame;
    [SerializeField] private GameEventSO onPauseOptions;
    [SerializeField] private GameEventSO onLeaveDungeon;
    [SerializeField] private GameEventSO onPauseExitGame;

    private VisualElement root;
    public VisualElement Root => root;

    private Button continueButton;
    private Button optionsButton;
    private Button leaveDungeonButton;
    private Button exitButton;



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

        onPauseGame.Subscribe(ActivatePauseUI);
    }

    private void OnDisable()
    {
        continueButton.clicked -= ContinueButton_clicked;
        optionsButton.clicked -= OptionsButton_clicked;
        leaveDungeonButton.clicked -= LeaveDungeonButton_clicked;
        exitButton.clicked -= ExitButton_clicked;

        onPauseGame.UnSubscribe(ActivatePauseUI);
    }

    private void ContinueButton_clicked()
    {
        uiManager.CloseTopUI();
    }

    private void OptionsButton_clicked()
    {
        onPauseOptions.Invoke();
    }

    private void LeaveDungeonButton_clicked()
    {
        onLeaveDungeon.Invoke();
    }

    private void ExitButton_clicked()
    {
        onPauseExitGame.Invoke();
    }

    private void ActivatePauseUI()
    {
        uiManager.OpenUI(root, true);
    }
}
