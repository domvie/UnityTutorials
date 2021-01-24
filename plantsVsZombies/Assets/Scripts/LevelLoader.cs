using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] int splashScreenDuration = 3;
    int currentSceneIndex;

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            LoadStartScreen();
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void LoadStartScreen()
    {
        StartCoroutine(LoadSceneAfterSecs(splashScreenDuration, "Start Screen"));
    }

    IEnumerator LoadSceneAfterSecs(int seconds, string scene)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(scene);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
