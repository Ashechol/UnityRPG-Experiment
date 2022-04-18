using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveManager : Singleton<SaveManager>
{
    string sceneKey = "scene";
    public string SavedScene { get { return PlayerPrefs.GetString(sceneKey); } }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SavePlayerData();
            SceneLoadManager.Instance.LoadMainMenu();
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SavePlayerData();
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadPlayerData();
        }
    }

    public void SavePlayerData()
    {
        Save(GameManager.Instance.playerStats.characterData,
             GameManager.Instance.playerStats.characterData.name);
    }

    public void LoadPlayerData()
    {
        Debug.Log("Loaded scene:" + PlayerPrefs.GetString(sceneKey));
        Load(GameManager.Instance.playerStats.characterData,
             GameManager.Instance.playerStats.characterData.name);
    }

    public void Save(Object data, string key)
    {
        var jsonData = JsonUtility.ToJson(data, true);
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.SetString(sceneKey, SceneManager.GetActiveScene().name);
        Debug.Log("saved scene:" + PlayerPrefs.GetString(sceneKey));
        PlayerPrefs.Save();
    }

    public void Load(Object data, string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), data);
        }
    }
}
