using System;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDataPersistence
{
    public static PlayerManager Instance { get; private set; } //Only instance of this class to become a singleton
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public PlayerData playerData;

    public void InitPlayerManager()
    {
        playerData.InitPlayerData();
    }

    #region IDataPersistence
    public void LoadData(GameData gameData)
    {
        playerData.canPlayerInput = gameData.playerData.canPlayerInput;
        playerData.worldPosition = gameData.playerData.worldPosition;
        playerData.playerStats = gameData.playerData.playerStats;
        
        LoadCreatureData(playerData.birdData, gameData.playerData.birdData);
        LoadSpellData(playerData.blazeBallData, gameData.playerData.blazeBallData);
        LoadWeaponData(playerData.beginnersBowData, gameData.playerData.beginnersBowData);
        LoadWeaponData(playerData.simpleSwordData, gameData.playerData.simpleSwordData);

        playerData.activeDeck = gameData.playerData.activeDeck;
    }

    public void SaveData(GameData gameData)
    {
        gameData.playerData.canPlayerInput = playerData.canPlayerInput;
        gameData.playerData.worldPosition = playerData.worldPosition;
        gameData.playerData.playerStats = playerData.playerStats;
        
        SaveCreatureData(playerData.birdData, gameData.playerData.birdData);
        SaveSpellData(playerData.blazeBallData, gameData.playerData.blazeBallData);
        SaveWeaponData(playerData.beginnersBowData, gameData.playerData.beginnersBowData);
        SaveWeaponData(playerData.simpleSwordData, gameData.playerData.simpleSwordData);

        gameData.playerData.activeDeck = playerData.activeDeck;
    }

    public void LoadCreatureData(CreatureData creatureData, SerializableCreatureData serializableCreatureData)
    {
        creatureData.cardProps = serializableCreatureData.cardProps;
        creatureData.cardStats = serializableCreatureData.cardStats;
        creatureData.creatureStats = serializableCreatureData.creatureStats;
    }

    public void SaveCreatureData(CreatureData creatureData, SerializableCreatureData serializableCreatureData)
    {   
        serializableCreatureData.cardProps = creatureData.cardProps;
        serializableCreatureData.cardStats = creatureData.cardStats;
        serializableCreatureData.creatureStats = creatureData.creatureStats;
    }

    public void LoadSpellData(SpellData spellData, SerializableSpellData serializableSpellData)
    {
        spellData.cardProps = serializableSpellData.cardProps;
        spellData.cardStats = serializableSpellData.cardStats;
        spellData.spellStats = serializableSpellData.spellStats;
    }

    public void SaveSpellData(SpellData spellData, SerializableSpellData serializablespellData)
    {   
        serializablespellData.cardProps = spellData.cardProps;
        serializablespellData.cardStats = spellData.cardStats;
        serializablespellData.spellStats = spellData.spellStats;
    }

    public void LoadWeaponData(WeaponData weaponData, SerializableWeaponData serializableWeaponData)
    {
        weaponData.cardProps = serializableWeaponData.cardProps;
        weaponData.cardStats = serializableWeaponData.cardStats;
        weaponData.weaponStats = serializableWeaponData.weaponStats;
    }

    public void SaveWeaponData(WeaponData weaponData, SerializableWeaponData serializableWeaponData)
    {   
        serializableWeaponData.cardProps = weaponData.cardProps;
        serializableWeaponData.cardStats = weaponData.cardStats;
        serializableWeaponData.weaponStats = weaponData.weaponStats;
    }
    #endregion

    private void Update()
    {
        try
        {
            for (int i = 0; i < playerData.deckSize; i++)
            {
                CardCooldownTimerTick(i);
                CardDurationTimerTick(i);
            }
        }
        catch (Exception) { }
    }

    public void CardCooldownTimerTick(int i)
    {
        if (playerData.activeDeck[i].GetStat(CARDSTATS.Cooldown).value > 0)
        {
            playerData.activeDeck[i].GetStat(CARDSTATS.Cooldown).value -= Time.deltaTime;
        }
        else if (playerData.activeDeck[i].GetStat(CARDSTATS.Cooldown).value <= 0)
        {
            playerData.activeDeck[i].GetStat(CARDSTATS.Cooldown).value = 0;
        }
    }

    public void CardDurationTimerTick(int i)
    {
        if (playerData.activeDeck[i].GetStat(CARDSTATS.Duration).value > 0)
        {
            playerData.activeDeck[i].GetStat(CARDSTATS.Duration).value -= Time.deltaTime;
        }
        else if (playerData.activeDeck[i].GetStat(CARDSTATS.Duration).value <= 0)
        {
            playerData.activeDeck[i].GetStat(CARDSTATS.Duration).value = 0;
        }
    }
}