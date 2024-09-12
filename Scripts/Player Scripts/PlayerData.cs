using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

[System.Serializable]
public class SerializablePlayerData
{
    [Header("Stats")]
    public Vector2 worldPosition;
    public List<Stat> playerStats;

    [Header("Cards & Decks")]
    public SerializableCreatureData birdData;
    public SerializableSpellData blazeBallData;
    public SerializableWeaponData beginnersBowData;
    public SerializableWeaponData simpleSwordData;
    public List<CardData> activeDeck;

    public SerializablePlayerData()
    {
        birdData = new SerializableCreatureData();
        blazeBallData = new SerializableSpellData();
        beginnersBowData = new SerializableWeaponData();
        simpleSwordData = new SerializableWeaponData();
        activeDeck = new List<CardData>();
    }
}

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Runtime")]
    public bool canPlayerInput;
    public enum EAnimState
    {
        Idle, Move, SummonCreature, CastSpell,
        SwordAttack, HeavyAttack, PolearmAttack, BowAttack, StaffAttack,
        Stunned,
    }
    public EAnimState animState;
    public readonly int deckSize = 4;
    public List<Creature> activeCreatureObjects;
    public List<Spell> activeSpellObjects;
    public Weapon activeWeaponObject;

    [Header("Stats")]
    public Vector2 worldPosition;
    public List<Stat> playerStats;

    [Header("Cards & Decks")]
    public CreatureData birdData;
    public SpellData blazeBallData;
    public WeaponData beginnersBowData;
    public WeaponData simpleSwordData;
    public List<CardData> activeDeck;

    public void InitPlayerData()
    {
        //Runtime
        canPlayerInput = true;
        animState = EAnimState.Idle;
        activeCreatureObjects = new List<Creature>();
        activeSpellObjects = new List<Spell>();
        activeWeaponObject = null;

        //Stats
        worldPosition = new Vector2(0, 0);
        playerStats = new List<Stat>();
        AddStat(EPlayerStats.PlayerDirection, 1f);
        AddStat(EPlayerStats.HitPoints, 20f);
        AddStat(EPlayerStats.AttackDamage, 10f);
        AddStat(EPlayerStats.AttackRange, 10f);
        AddStat(EPlayerStats.AttackSpeed, 1f);
        AddStat(EPlayerStats.MovementSpeed, 60f);

        //Cards
        birdData.InitCardData();
        blazeBallData.InitCardData();
        beginnersBowData.InitCardData();
        simpleSwordData.InitCardData();
        //Decks
        activeDeck = new List<CardData>();

        //From SO, changes in SO & GO is the same
        //birdData.InitCardData();

        //New SO, changes in SO & GO is different
        //PlayerBird newPlayerBird = (PlayerBird)CreateInstance("PlayerBird");
        //newPlayerBird.InitCardData();
    }

    public void AddStat(EPlayerStats stat, float maxValue)
    {
        playerStats.Insert((int)stat, new Stat(maxValue));
        GetStat(stat).name = stat.ToString();
    }

    public Stat GetStat(EPlayerStats stat)
    {
        return playerStats[(int)stat];
    }
}


