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

    [Header("Card Objects")]
    public Bird bird;
    public BlazeBall blazeBall;
    public BeginnersBow beginnersBow;
    public SimpleSword simpleSword;

    [Header("Deck")]
    public readonly int deckSize = 4;
    public List<CardObject> deck1;
    public List<CardObject> deck2;
    public List<CardObject> activeDeck;
    public List<CreatureObject> activeCreatureObjects;
    public List<SpellObject> activeSpellObjects;
    public WeaponObject activeWeaponObject;
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
    public PlayerBird playerBird;
    public PlayerBlazeBall playerBlazeBall;
    public PlayerBeginnersBow playerBeginnersBow;
    public PlayerSimpleSword playerSimpleSword;

    [Header("Card Objects")]
    public Bird bird;
    public BlazeBall blazeBall;
    public BeginnersBow beginnersBow;
    public SimpleSword simpleSword;
    
    [Header("Deck")]
    public readonly int deckSize = 4;
    public List<CardObject> activeDeck;
    public List<CardObject> activeCreatureObjects;
    public List<CardObject> activeSpellObjects;
    public CardObject activeWeaponObject;

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
        //From SO, changes in SO & GO is the same
        playerBird.InitCardData();
        //New SO, changes in SO & GO is different
        PlayerBird newPlayerBird = (PlayerBird)CreateInstance("PlayerBird");
        newPlayerBird.InitCardData();
    
        //Card Objects
        bird.InitCardObject((PlayerBird)CreateInstance("PlayerBird"));
        blazeBall.InitCardObject((PlayerBlazeBall)CreateInstance("PlayerBlazeBall"));
        beginnersBow.InitCardObject((PlayerBeginnersBow)CreateInstance("PlayerBeginnersBow"));
        simpleSword.InitCardObject((PlayerSimpleSword)CreateInstance("PlayerSimpleSword"));
    
        //Deck
        activeDeck = new List<CardObject>();
        activeDeck.Insert(0, bird);
        activeDeck.Insert(1, blazeBall);
        activeDeck.Insert(2, beginnersBow);
        activeDeck.Insert(3, simpleSword);
        activeCreatureObjects = new List<CardObject>();
        activeSpellObjects = new List<CardObject>();
        activeWeaponObject = null;
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


