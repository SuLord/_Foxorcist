using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI bestScore;
    [SerializeField] TextMeshProUGUI yourScore;
    [SerializeField] Button restartButton;
    private int m_Score;
    public int currentLives;

    public static int m_BestScore;

    public bool isGameActive;
    private void Start()
    {
        isGameActive = true;
        m_Score = 0;
        currentLives = 3;
        UpdateScore(0);
        UpdateLives();
    }
    public void UpdateLives()
    {
        livesText.text = "Lives: " + currentLives;
    }
    public void UpdateScore(int scoreToAdd)
    {
        m_Score += scoreToAdd;
        PlayerDataHandle.Instance.score = m_Score;
        scoreText.text = "Score: " + m_Score;
    }
    // Stop game, bring up game over screen UI and destroy enemies/player
    public void GameOver()
    {
        isGameActive = false;

        CheckBestPlayer();

        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        bestScore.gameObject.SetActive(true);
        yourScore.gameObject.SetActive(true);

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
        SetBestPlayer();
        yourScore.text = $"Your score : {m_Score}";
    }
    // Scoring methods
    private void CheckBestPlayer()
    {
        int CurrentScore = PlayerDataHandle.Instance.score;

        if (CurrentScore > m_BestScore)
        {
            m_BestScore = CurrentScore;

            bestScore.text = $"Best score : {m_BestScore}";

            SaveGameRank(m_BestScore);
        }
    }
    private void SetBestPlayer()
    {
        if (m_BestScore == 0)
        {
            bestScore.text = "";
        }
        else
        {
            bestScore.text = $"Best score : {m_BestScore}";
        }
    }
    public void SaveGameRank(int bestPlayerScore)
    {
        SaveData data = new SaveData();

        data.HighestScore = bestPlayerScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadGameRank()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            m_BestScore = data.HighestScore;
        }
    }
    [System.Serializable]
    class SaveData
    {
        public int HighestScore;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
