using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IDataPersistence
{
    public static GameManager Instance { get; private set; }
    public Card card;

    public SettingsData settingsData;
    public Sprite[] manaTypeSprites;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        card = new Card();
    }

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

    /*
    private void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            PlayerManager.Instance.playerData.handCards[0].GetStat(CARDSTATS.Mana).value += 0.12f;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            PlayerManager.Instance.playerData.handCards[1].GetStat(CARDSTATS.Mana).value += 0.12f;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerManager.Instance.playerData.handCards[2].GetStat(CARDSTATS.Mana).value += 0.12f;
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            PlayerManager.Instance.playerData.handCards[3].GetStat(CARDSTATS.Mana).value += 0.12f;
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            //playerData = (PlayerData)ScriptableObject.CreateInstance("PlayerData");
        }
    }
    */  
    #endregion
}

public class Card
{   
    public enum CARDCATEGORIES
    {
        CREATURE, SPELL, WEAPON,
    }
    public readonly Dictionary<CARDCATEGORIES, string> cardCategoryDict = new()
    {
        { CARDCATEGORIES.CREATURE, "Creature" }, { CARDCATEGORIES.SPELL, "Spell" }, { CARDCATEGORIES.WEAPON, "Weapon" }
    };

    public enum MANATYPES
    {
        AETHER, EARTH, FIRE, NETHER, WATER
    }
    public readonly Dictionary<MANATYPES, string> manaTypesDict = new()
    {
        {MANATYPES.AETHER, "Aether" }, {MANATYPES.EARTH, "Earth" }, {MANATYPES.FIRE, "Fire" },
        {MANATYPES.NETHER, "Nether" }, {MANATYPES.WATER, "Water"}
    };

    public enum CREATURETYPES
    {

    }
    public readonly Dictionary<CREATURETYPES, string> creatureTypeDict = new()
    {

    };

    public enum SPELLTYPES
    {

    }
    public readonly Dictionary<SPELLTYPES, string> spellTypeDict = new()
    {

    };

    public enum WEAPONTYPES
    {
        SWORD, HEAVY, POLEARM, BOW, STAFF
    }
    public readonly Dictionary<WEAPONTYPES, string> weaponTypeDict = new()
    {
        { WEAPONTYPES.SWORD, "Sword" }, { WEAPONTYPES.HEAVY, "Heavy" }, { WEAPONTYPES.POLEARM, "Polearm" },
        { WEAPONTYPES.BOW, "Bow" }, { WEAPONTYPES.STAFF, "Staff" }
    };
}