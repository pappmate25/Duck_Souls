using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //Events
    [SerializeField] private GameEventSO onStartGame;
    [SerializeField] private GameEventSO onExitGame;

    [SerializeField] private GameEventIntSO onDungeonSelect;

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
        int nextScene = currentSceneIndex + 1;

        if(nextScene <= SceneManager.loadedSceneCount)
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.Log("ERROR: Could not find the next Scene!");
            SceneManager.LoadScene(0);
        }
    }

    private void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }

    private void LoadHub()
    {
        SceneManager.LoadScene(hubIndex);
    }

    private void LoadDungeon(int dungeonIndex)
    {
        SceneManager.LoadScene(currentSceneIndex + dungeonIndex);
    }
}
