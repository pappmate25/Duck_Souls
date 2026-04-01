using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneDataSO sceneData;
    [SerializeField] private DungeonProgressSO dungeonProgress;

    [Header("Events")]
    [SerializeField] private GameEventSO NewGameWarningPopup_onStartGame;

    //MainMenuUIController
    [SerializeField] private GameEventSO MainMenuUIController_onContinueGame;
    [SerializeField] private GameEventSO MainMenuUIController_onExitGame;

    //DungeonSelectUI
    [SerializeField] private GameEventIntSO DungeonSelectUI_onDungeonSelect;

    //PauseMenuController
    [SerializeField] private GameEventSO PauseMenuController_onLeaveDungeon;
    [SerializeField] private GameEventSO PauseMenuController_onExitGame;

    [SerializeField] private GameEventSO DeathSummaryUI_onReturnToHub;


    private void OnEnable()
    {
        NewGameWarningPopup_onStartGame.Subscribe(StartGame);

        MainMenuUIController_onContinueGame.Subscribe(ContinueGame);
        MainMenuUIController_onExitGame.Subscribe(ExitGame);

        DungeonSelectUI_onDungeonSelect.Subscribe(LoadDungeon);

        PauseMenuController_onLeaveDungeon.Subscribe(LoadHub);
        PauseMenuController_onExitGame.Subscribe(ExitGame);

        DeathSummaryUI_onReturnToHub.Subscribe(LoadHub);
    }

    private void OnDisable()
    {
        NewGameWarningPopup_onStartGame.UnSubscribe(StartGame);
        MainMenuUIController_onContinueGame.UnSubscribe(ContinueGame);
        MainMenuUIController_onExitGame.UnSubscribe(ExitGame);

        DungeonSelectUI_onDungeonSelect.UnSubscribe(LoadDungeon);

        PauseMenuController_onLeaveDungeon.UnSubscribe(LoadHub);
        PauseMenuController_onExitGame.UnSubscribe(ExitGame);

        DeathSummaryUI_onReturnToHub.UnSubscribe(LoadHub);
    }

    private void StartGame()
    {
        dungeonProgress.ResetAllProgress();
        SceneManager.LoadScene(sceneData.HubScene);
    }

    private void ContinueGame()
    {
        dungeonProgress.LoadProgress();
        SceneManager.LoadScene(sceneData.HubScene);
    }

    private void LoadHub()
    {
        SceneManager.LoadScene(sceneData.HubScene);
    }

    private void LoadDungeon(int dungeonIndex)
    {
        string scene = sceneData.GetDungeonScene(dungeonIndex);

        if(scene != null)
        {
            SceneManager.LoadScene(scene);
        }
    }

    private void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }
}
