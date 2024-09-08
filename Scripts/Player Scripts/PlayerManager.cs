using System;
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
        playerData.playerStats = gameData.playerData.playerStats;
        playerData.worldPosition = gameData.playerData.worldPosition;
        //playerData.deck = gameData.playerData.deck;
        //playerData.handCards = gameData.playerData.handCards;
    }

    public void SaveData(GameData gameData)
    {
        gameData.playerData.canPlayerInput = playerData.canPlayerInput;
        gameData.playerData.playerStats = playerData.playerStats;
        gameData.playerData.worldPosition = playerData.worldPosition;
        //gameData.playerData.deck = playerData.deck;
        //gameData.playerData.handCards = playerData.handCards;
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
        if (playerData.activeDeck[i].GetCardStat(CARDSTATS.Cooldown).value > 0)
        {
            playerData.activeDeck[i].GetCardStat(CARDSTATS.Cooldown).value -= Time.deltaTime;
        }
        else if (playerData.activeDeck[i].GetCardStat(CARDSTATS.Cooldown).value <= 0)
        {
            playerData.activeDeck[i].GetCardStat(CARDSTATS.Cooldown).value = 0;
        }
    }

    public void CardDurationTimerTick(int i)
    {
        if (playerData.activeDeck[i].GetCardStat(CARDSTATS.Duration).value > 0)
        {
            playerData.activeDeck[i].GetCardStat(CARDSTATS.Duration).value -= Time.deltaTime;
        }
        else if (playerData.activeDeck[i].GetCardStat(CARDSTATS.Duration).value <= 0)
        {
            playerData.activeDeck[i].GetCardStat(CARDSTATS.Duration).value = 0;
        }
    }
    
}