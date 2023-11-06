using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameData
{
    public int highestWave;
    public int totalWaves;
    public int highestKillAmount;
    public int totalKills;
    public int gamesPlayed;
}

public class GameManager : MonoBehaviour
{
    public GameData gameData;
    private string saveFilePath;

    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        saveFilePath = Application.persistentDataPath + "/GameData.json";

        LoadGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            SaveGame();
        }
            
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
            
        if (Input.GetKeyDown(KeyCode.M))
        {
            DeleteSaveFile();
        }   
    }

    public void SaveGame()
    {
        string savePlayerData = JsonUtility.ToJson(gameData);
        File.WriteAllText(saveFilePath, savePlayerData);

        Debug.Log("Save file created at: " + saveFilePath);
    }

    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string loadGameData = File.ReadAllText(saveFilePath);
            gameData = JsonUtility.FromJson<GameData>(loadGameData);

            Debug.Log("Load game complete! \nTotal kills: " + gameData.totalKills + "Highest kills: " + gameData.highestKillAmount +
                "Total waves: " + gameData.totalWaves + "Highest wave: " + gameData.highestWave + "Total games played: " + gameData.gamesPlayed);
        }
        else
        {
            gameData = new GameData();
            Debug.Log("There is no save files to load!");
        }
    }

    public void DeleteSaveFile()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);

            Debug.Log("Save file deleted!");
        }
        else
            Debug.Log("There is nothing to delete!");
    }
}
