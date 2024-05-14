using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    private int score;
    public int currentLives;

    public bool isGameActive;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        score = 0;
        currentLives = 3;
        UpdateScore(0);
        UpdateLives();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives()
    {
        livesText.text = "Lives: " + currentLives;
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
        string[] tagsToDisable =
            {
                "Enemy",
                "PowerUp",
            };
        foreach (string tag in tagsToDisable)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject gameObj in gameObjects)
            {
                Destroy(gameObj);
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
