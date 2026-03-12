using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int currentSceneIndex;

    private void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnEnable()
    {
        MainMenuUIController.OnStartGame += StartGame;
        MainMenuUIController.OnExitGame += ExitGame;
    }

    private void OnDisable()
    {
        MainMenuUIController.OnStartGame -= StartGame;
        MainMenuUIController.OnExitGame -= ExitGame;
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
}
