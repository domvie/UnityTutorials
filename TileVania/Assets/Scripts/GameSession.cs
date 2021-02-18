using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] Text scoreText;
    [SerializeField] Text livesText;


    void Awake() {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void AddToScore(int amount) {
        score += amount;
        scoreText.text = score.ToString();
    }


    public void ProcessPlayerDeath() {
        if (playerLives > 1) {
            TakeLife();
        } else {
            ResetGameSession();
        }
    }

    private void ResetGameSession() {
        SceneManager.LoadScene(2);
        Destroy(gameObject);
    }

    public void TakeLife() {
        playerLives -= 1;
        livesText.text = playerLives.ToString();
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}
