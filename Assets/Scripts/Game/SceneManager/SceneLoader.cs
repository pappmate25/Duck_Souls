using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int currentSceneIndex;
    private int hubIndex = 1;

    private void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnEnable()
    {
        MainMenuUIController.OnStartGame += StartGame;
        MainMenuUIController.OnExitGame += ExitGame;

        DungeonSelectUI.OnDungeonSelect += LoadDungeon;

        PauseMenuController.OnLeaveDungeon += LoadHub;
        PauseMenuController.OnExitGame += ExitGame;

        ExitDoor.OnReturnToHub += LoadHub;
    }

    private void OnDisable()
    {
        MainMenuUIController.OnStartGame -= StartGame;
        MainMenuUIController.OnExitGame -= ExitGame;

        DungeonSelectUI.OnDungeonSelect -= LoadDungeon;

        PauseMenuController.OnLeaveDungeon -= LoadHub;
        PauseMenuController.OnExitGame -= ExitGame;

        ExitDoor.OnReturnToHub -= LoadHub;
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
