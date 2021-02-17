using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    
    void OnTriggerEnter2D() {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel() {
        yield return new WaitForSeconds(3);

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
