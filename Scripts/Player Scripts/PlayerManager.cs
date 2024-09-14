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
        playerData.activeDeck.Insert(0, playerData.birdData);
        playerData.activeDeck.Insert(1, playerData.blazeBallData);
        playerData.activeDeck.Insert(2, playerData.beginnersBowData);
        playerData.activeDeck.Insert(3, playerData.simpleSwordData);
    }

    #region IDataPersistence
    public void LoadData(GameData gameData)
    {
        playerData.worldPosition = gameData.playerData.worldPosition;
        playerData.playerStats = gameData.playerData.playerStats;
        LoadCreatureData(playerData.birdData, gameData.playerData.birdData);
        LoadSpellData(playerData.blazeBallData, gameData.playerData.blazeBallData);
        LoadWeaponData(playerData.beginnersBowData, gameData.playerData.beginnersBowData);
        LoadWeaponData(playerData.simpleSwordData, gameData.playerData.simpleSwordData);

        for (int i = 0; i < playerData.deckSize; i++)
        {
            //serializableCardData InstanceID was overridden to match the new cardData InstanceID, so we find & assign again the cardData
            if (gameData.playerData.activeDeck[i]) 
            {
                playerData.activeDeck[i] = FindCardDataByID((int)gameData.playerData.activeDeck[i].GetStat(ECardStats.InstanceID).value);
            }
        }
    }

    public void SaveData(GameData gameData)
    {
        gameData.playerData.worldPosition = playerData.worldPosition;
        gameData.playerData.playerStats = playerData.playerStats;
        SaveCreatureData(playerData.birdData, gameData.playerData.birdData);
        SaveSpellData(playerData.blazeBallData, gameData.playerData.blazeBallData);
        SaveWeaponData(playerData.beginnersBowData, gameData.playerData.beginnersBowData);
        SaveWeaponData(playerData.simpleSwordData, gameData.playerData.simpleSwordData);
        gameData.playerData.activeDeck = playerData.activeDeck;
    }

    public CardData FindCardDataByID(int id)
    {
        CardData cardData = null;
        if (id == playerData.birdData.GetStat(ECardStats.InstanceID).value) cardData = playerData.birdData;
        else if (id == playerData.blazeBallData.GetStat(ECardStats.InstanceID).value) cardData = playerData.blazeBallData;
        else if (id == playerData.beginnersBowData.GetStat(ECardStats.InstanceID).value) cardData = playerData.beginnersBowData;
        else if (id == playerData.simpleSwordData.GetStat(ECardStats.InstanceID).value) cardData = playerData.simpleSwordData;
        else Debug.Log("Cannot find card data with this ID: " + id);
        return cardData;
    }

    public void LoadCreatureData(CreatureData creatureData, SerializableCreatureData serializableCreatureData)
    {
        //Override serializableCreatureData.cardStats[0].value which contains the serializableCreatureData InstanceID 
        //to match the new creatureData InstanceID, also call DataManager.Instance.SaveGame() to override also the GameData File
        serializableCreatureData.cardStats[0].value = creatureData.GetInstanceID();
        creatureData.cardProps = serializableCreatureData.cardProps;
        creatureData.cardStats = serializableCreatureData.cardStats;
        creatureData.creatureStats = serializableCreatureData.creatureStats;
    }

    public void LoadSpellData(SpellData spellData, SerializableSpellData serializableSpellData)
    {
        //Override serializableSpellData.cardStats[0].value which contains the serializableSpellData InstanceID 
        //to match the new creatureData InstanceID, also call DataManager.Instance.SaveGame() to override also the GameData File
        serializableSpellData.cardStats[0].value = spellData.GetInstanceID();
        spellData.cardProps = serializableSpellData.cardProps;
        spellData.cardStats = serializableSpellData.cardStats;
        spellData.spellStats = serializableSpellData.spellStats;
    }

    public void LoadWeaponData(WeaponData weaponData, SerializableWeaponData serializableWeaponData)
    {
        //Override serializableWeaponData.cardStats[0].value which contains the serializableWeaponData InstanceID 
        //to match the new creatureData InstanceID, also call DataManager.Instance.SaveGame() to override also the GameData File
        serializableWeaponData.cardStats[0].value = weaponData.GetInstanceID();
        weaponData.cardProps = serializableWeaponData.cardProps;
        weaponData.cardStats = serializableWeaponData.cardStats;
        weaponData.weaponStats = serializableWeaponData.weaponStats;
    }

    public void SaveCreatureData(CreatureData creatureData, SerializableCreatureData serializableCreatureData)
    {
        serializableCreatureData.cardProps = creatureData.cardProps;
        serializableCreatureData.cardStats = creatureData.cardStats;
        serializableCreatureData.creatureStats = creatureData.creatureStats;
    }

    public void SaveSpellData(SpellData spellData, SerializableSpellData serializablespellData)
    {
        serializablespellData.cardProps = spellData.cardProps;
        serializablespellData.cardStats = spellData.cardStats;
        serializablespellData.spellStats = spellData.spellStats;
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
        if (playerData.activeDeck[i].GetStat(ECardStats.Cooldown).value > 0)
        {
            playerData.activeDeck[i].GetStat(ECardStats.Cooldown).value -= Time.deltaTime;
        }
        else if (playerData.activeDeck[i].GetStat(ECardStats.Cooldown).value <= 0)
        {
            playerData.activeDeck[i].GetStat(ECardStats.Cooldown).value = 0;
        }
    }

    public void CardDurationTimerTick(int i)
    {
        if (playerData.activeDeck[i].GetStat(ECardStats.Duration).value > 0)
        {
            playerData.activeDeck[i].GetStat(ECardStats.Duration).value -= Time.deltaTime;
        }
        else if (playerData.activeDeck[i].GetStat(ECardStats.Duration).value <= 0)
        {
            playerData.activeDeck[i].GetStat(ECardStats.Duration).value = 0;
        }
    }
}