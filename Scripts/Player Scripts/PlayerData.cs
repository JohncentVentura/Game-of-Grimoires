using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializablePlayerData
{
    [Header("Stats & State")]
    public bool canPlayerInput;
    public Vector2 worldPosition;
    public List<Stat> playerStats;
    public enum AnimState
    {
        IDLE, MOVE, SUMMON_CREATURE, CAST_SPELL,
        SWORD_ATK, POLEARM_ATK, HEAVY_ATK, BOW_ATK, STAFF_ATK,
        STUNNED,
    }
    public AnimState animState;

    [Header("Serializable Card Data")]
    public SerializableCreatureData birdData;
    public SerializableSpellData blazeBallData;
    public SerializableWeaponData beginnersBowData;
    public SerializableWeaponData simpleSwordData;

    [Header("Deck")]
    public readonly int deckSize = 4;
    public List<CardData> activeDeck;
    public List<CreatureObject> activeCreatureObjects;
    public List<SpellObject> activeSpellObjects;
    public WeaponObject activeWeaponObject;

    public SerializablePlayerData()
    {
        birdData = new SerializableCreatureData();
        blazeBallData = new SerializableSpellData();
        beginnersBowData = new SerializableWeaponData();
        simpleSwordData = new SerializableWeaponData();
    }
}

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Stats & State")]
    public bool canPlayerInput;
    public Vector2 worldPosition;
    public List<Stat> playerStats;
    public enum AnimState
    {
        IDLE, MOVE, SUMMON_CREATURE, CAST_SPELL,
        SWORD_ATK, POLEARM_ATK, HEAVY_ATK, BOW_ATK, STAFF_ATK,
        STUNNED,
    }
    public AnimState animState;

    [Header("Card Data")]
    public CreatureData birdData;
    public SpellData blazeBallData;
    public WeaponData beginnersBowData;
    public WeaponData simpleSwordData;

    [Header("Deck")]
    public readonly int deckSize = 4;
    public List<CardData> activeDeck;
    public List<CreatureObject> activeCreatureObjects;
    public List<SpellObject> activeSpellObjects;
    public WeaponObject activeWeaponObject;

    public void InitPlayerData()
    {
        //Stats & State
        canPlayerInput = true;
        worldPosition = new Vector2(0, 0);
        playerStats = new List<Stat>();
        AddStat(PLAYERSTATS.PlayerDirection, 1f);
        AddStat(PLAYERSTATS.HitPoints, 20f);
        AddStat(PLAYERSTATS.AttackDamage, 10f);
        AddStat(PLAYERSTATS.AttackRange, 10f);
        AddStat(PLAYERSTATS.AttackSpeed, 1f);
        AddStat(PLAYERSTATS.MovementSpeed, 60f);
        animState = AnimState.IDLE;

        //Card Data
        birdData.InitCardData();
        blazeBallData.InitCardData();
        beginnersBowData.InitCardData();
        simpleSwordData.InitCardData();

        //Deck
        activeDeck = new List<CardData>();
        activeDeck.Insert(0, birdData);
        activeDeck.Insert(1, blazeBallData);
        activeDeck.Insert(2, beginnersBowData);
        activeDeck.Insert(3, simpleSwordData);
        activeCreatureObjects = new List<CreatureObject>();
        activeSpellObjects = new List<SpellObject>();
        activeWeaponObject = null;

        //From SO, changes in SO & GO is the same
        //birdData.InitCardData();

        //New SO, changes in SO & GO is different
        //PlayerBird newPlayerBird = (PlayerBird)CreateInstance("PlayerBird");
        //newPlayerBird.InitCardData();
    }

    public void AddStat(PLAYERSTATS stat, float maxValue)
    {
        playerStats.Insert((int)stat, new Stat(maxValue));
        GetStat(stat).name = stat.ToString();
    }

    public Stat GetStat(PLAYERSTATS stat)
    {
        return playerStats[(int)stat];
    }
}


