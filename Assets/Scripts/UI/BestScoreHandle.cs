using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;

public class BestScoreHandle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bestScore;
    private void Awake()
    {
        LoadGameRank();
    }
    void Start()
    {
        SetBestPlayer();
    }
    private void SetBestPlayer()
    {
        if (GameManager.m_BestScore == 0)
        {
            bestScore.text = "";
        }
        else
        {
            bestScore.text = $"Best Score : {GameManager.m_BestScore}";
        }
    }
    public void LoadGameRank()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            GameManager.m_BestScore = data.HighestScore;
        }
    }
    [System.Serializable]
    class SaveData
    {
        public int HighestScore;
        public string TheBestPlayer;
    }
}
