using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneDataSO sceneData;

    //Events
    //MainMenuUIController
    [SerializeField] private GameEventSO onStartGame;
    [SerializeField] private GameEventSO onExitGame;

    //DungeonSelectUI
    [SerializeField] private GameEventIntSO onDungeonSelect;

    //PauseMenuController
    [SerializeField] private GameEventSO onLeaveDungeon;
    [SerializeField] private GameEventSO onPauseExitGame;

    private int currentSceneIndex;
    private int hubIndex = 1;

    private void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnEnable()
    {
        onStartGame.Subscribe(StartGame);
        onExitGame.Subscribe(ExitGame);

        onDungeonSelect.Subscribe(LoadDungeon);

        onLeaveDungeon.Subscribe(LoadHub);
        onPauseExitGame.Subscribe(ExitGame);

        //ExitDoor.OnReturnToHub += LoadHub;
    }

    private void OnDisable()
    {
        onStartGame.UnSubscribe(StartGame);
        onExitGame.UnSubscribe(ExitGame);

        onDungeonSelect.UnSubscribe(LoadDungeon);

        onLeaveDungeon.UnSubscribe(LoadHub);
        onPauseExitGame.UnSubscribe(ExitGame);

        //ExitDoor.OnReturnToHub -= LoadHub;
    }

    private void StartGame()
    {
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
