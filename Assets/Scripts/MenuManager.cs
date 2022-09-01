using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public TMP_InputField inputField;
    public TMP_Text BestScoreText;
    public string userName;
    public string lUserName;
    public int score;
    private void Awake() 
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadPlayer();
        BestScoreText.text = "Best Score: " + lUserName + " : " + userName;
    }
    public void SetUserName()
    {
        userName = inputField.text;
    }

    public string GetUserName()
    {
        return userName;
    }
    
    public int GetScore()
    {
        return score;
    }
    [System.Serializable]
    class SaveData
    {
        public string playerUserName;
        public int score;
    }
    public void LoginPlayer(string name ,int scores)
    {
        SaveData data = new SaveData();
        data.playerUserName = GetUserName();
        data.score = scores;
    }
    public void SavePlayer(string name ,int scores)
    {
        SaveData data = new SaveData();
        data.playerUserName = name;
        data.score = scores;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json",json);
    }
    public void LoadPlayer()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            lUserName = data.playerUserName;
            score = data.score;
        }
    }
}
