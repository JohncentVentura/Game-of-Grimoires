using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IDataPersistence
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public SettingsData settingsData;

    public void Start()    
    {
        if (DataManager.Instance.useWithoutGameData)
        {
            PlayerManager.Instance.InitPlayerManager();
        }
    }

    public void OnClickNewGame(string nextSceneName) //Called in StareMenuScene
    {
        settingsData.InitSettingsData();
        PlayerManager.Instance.InitPlayerManager();

        DataManager.Instance.NewGame();
        SceneManager.LoadScene(nextSceneName);
    }

    public void OnClickLoadGame(string nextSceneName) //Called in StareMenuScene
    {
        DataManager.Instance.LoadGame();
        SceneManager.LoadScene(nextSceneName);

        PlayerManager.Instance.playerData.canPlayerInput = true;
    }

    void OnApplicationQuit()
    {
        PlayerManager.Instance.playerData.canPlayerInput = false;
        PlayerManager.Instance.playerData.activeCreatureObjects = null;
        PlayerManager.Instance.playerData.activeSpellObjects = null;
        PlayerManager.Instance.playerData.activeWeaponObject = null;

        if (!DataManager.Instance.useWithoutGameData)
        {
            DataManager.Instance.SaveGame();
        }
    }

    #region IDataPersistence
    public void LoadData(GameData gameData)
    {   
        settingsData.movementInputKeys = gameData.settingsData.movementInputKeys;
        settingsData.cardInputKeys = gameData.settingsData.cardInputKeys;
        settingsData.attackInputKey = gameData.settingsData.attackInputKey;
    }

    public void SaveData(GameData gameData)
    {
        gameData.settingsData.movementInputKeys = settingsData.movementInputKeys;
        gameData.settingsData.cardInputKeys = settingsData.cardInputKeys;
        gameData.settingsData.attackInputKey = settingsData.attackInputKey;
    }
    #endregion

    #region Testing
    public void InvokeCallback(string input, Action callback)
    {
        if (Input.GetButtonDown(input)) callback?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            PlayerManager.Instance.playerData.activeDeck[0].GetStat(ECardStats.Mana).value += 0.12f;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            PlayerManager.Instance.playerData.activeDeck[1].GetStat(ECardStats.Mana).value += 0.12f;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerManager.Instance.playerData.activeDeck[2].GetStat(ECardStats.Mana).value += 0.12f;
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            PlayerManager.Instance.playerData.activeDeck[3].GetStat(ECardStats.Mana).value += 0.12f;
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            //playerData = (PlayerData)ScriptableObject.CreateInstance("PlayerData");
        }
    }
    #endregion
}