using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneDataSO sceneData;
    [SerializeField] private DungeonProgressSO dungeonProgress;

    [Header("Events")]
    //MainMenuUIController
    [SerializeField] private GameEventSO MainMenuUIController_onStartGame;
    [SerializeField] private GameEventSO MainMenuUIController_onExitGame;

    //DungeonSelectUI
    [SerializeField] private GameEventIntSO DungeonSelectUI_onDungeonSelect;

    //PauseMenuController
    [SerializeField] private GameEventSO PauseMenuController_onLeaveDungeon;
    [SerializeField] private GameEventSO PauseMenuController_onExitGame;

    [SerializeField] private GameEventSO ReturnToHubEvent;


    private void OnEnable()
    {
        MainMenuUIController_onStartGame.Subscribe(StartGame);
        MainMenuUIController_onExitGame.Subscribe(ExitGame);

        DungeonSelectUI_onDungeonSelect.Subscribe(LoadDungeon);

        PauseMenuController_onLeaveDungeon.Subscribe(LoadHub);
        PauseMenuController_onExitGame.Subscribe(ExitGame);

        ReturnToHubEvent.Subscribe(LoadHub);
    }

    private void OnDisable()
    {
        MainMenuUIController_onStartGame.UnSubscribe(StartGame);
        MainMenuUIController_onExitGame.UnSubscribe(ExitGame);

        DungeonSelectUI_onDungeonSelect.UnSubscribe(LoadDungeon);

        PauseMenuController_onLeaveDungeon.UnSubscribe(LoadHub);
        PauseMenuController_onExitGame.UnSubscribe(ExitGame);

        ReturnToHubEvent.UnSubscribe(LoadHub);
    }

    private void StartGame()
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
