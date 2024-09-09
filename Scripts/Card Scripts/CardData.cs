using System;
using System.Collections.Generic;
using UnityEngine;

/*
    CardData is the base value, override it by creating a new CardData Asset
*/
#region Serializable Cards
[Serializable]
public class SerializableCardData
{
    public List<Property> cardProps;
    public List<Stat> cardStats;
}

[Serializable]
public class SerializableCreatureData : SerializableCardData
{
    public List<Stat> creatureStats;

    public SerializableCreatureData()
    {
        cardProps = new List<Property>();
        cardStats = new List<Stat>();
        creatureStats = new List<Stat>();
    }
}

[Serializable]
public class SerializableSpellData : SerializableCardData
{
    public List<Stat> spellStats;

    public SerializableSpellData()
    {
        cardProps = new List<Property>();
        cardStats = new List<Stat>();
        spellStats = new List<Stat>();
    }
}

[Serializable]
public class SerializableWeaponData : SerializableCardData
{
    public List<Stat> weaponStats;

    public SerializableWeaponData()
    {
        cardProps = new List<Property>();
        cardStats = new List<Stat>();
        weaponStats = new List<Stat>();
    }
}
#endregion

public class CardData : ScriptableObject
{
    public Sprite cardSprite;
    public List<Property> cardProps;
    public List<Stat> cardStats;

    public void AddProp(CARDPROPS prop, string propName)
    {
        cardProps.Insert((int)prop, new Property(propName));
        GetProp(prop).name = prop.ToString(); //Renames element name in Unity Inspector
    }

    public void AddStat(CARDSTATS stat, float maxValue)
    {
        cardStats.Insert((int)stat, new Stat(maxValue));
        GetStat(stat).name = stat.ToString(); //Renames element name in Unity Inspector
    }

    public Property GetProp(CARDPROPS prop) => cardProps[(int)prop];
    public Stat GetStat(CARDSTATS stat) => cardStats[(int)stat];

    public virtual void InitCardData() { } //Override in CreatureData, SpellData, & WeaponData
    public virtual void AddStat(CREATURESTATS stat, float maxValue) { } //Override in CreatureData
    public virtual void AddStat(SPELLSTATS stat, float maxValue) { } //Override in SpellData
    public virtual void AddStat(WEAPONSTATS stat, float maxValue) { } //Override in WeaponData
    public virtual Stat GetStat(CREATURESTATS stat) => null; //Override in CreatureData
    public virtual Stat GetStat(SPELLSTATS stat) => null; //Override in SpellData
    public virtual Stat GetStat(WEAPONSTATS stat) => null; //Override in WeaponData
}

public class CreatureData : CardData
{
    public List<Stat> creatureStats;

    public override void InitCardData()
    {
        cardProps = new List<Property>();
        AddProp(CARDPROPS.MainType, GameManager.Instance.card.cardCategoryDict[Card.CARDCATEGORIES.CREATURE]);
        cardStats = new List<Stat>();
        creatureStats = new List<Stat>();
    }

    public override void AddStat(CREATURESTATS stat, float maxValue)
    {
        creatureStats.Insert((int)stat, new Stat(maxValue));
        GetStat(stat).name = stat.ToString(); //Renames element name in Unity Inspector
    }

    public override Stat GetStat(CREATURESTATS stat) => creatureStats[(int)stat];
}

public class SpellData : CardData
{
    public List<Stat> spellStats;

    public override void InitCardData()
    {
        cardProps = new List<Property>();
        AddProp(CARDPROPS.MainType, GameManager.Instance.card.cardCategoryDict[Card.CARDCATEGORIES.SPELL]);
        cardStats = new List<Stat>();
        spellStats = new List<Stat>();
    }

    public override void AddStat(SPELLSTATS stat, float maxValue)
    {
        spellStats.Insert((int)stat, new Stat(maxValue));
        GetStat(stat).name = stat.ToString(); //Renames element name in Unity Inspector
    }

    public override Stat GetStat(SPELLSTATS stat) => spellStats[(int)stat];
}

public class WeaponData : CardData
{
    public List<Stat> weaponStats;

    public override void InitCardData()
    {
        cardProps = new List<Property>();
        AddProp(CARDPROPS.MainType, GameManager.Instance.card.cardCategoryDict[Card.CARDCATEGORIES.WEAPON]);
        cardStats = new List<Stat>();
        weaponStats = new List<Stat>();
    }

    public override void AddStat(WEAPONSTATS stat, float maxValue)
    {
        weaponStats.Insert((int)stat, new Stat(maxValue));
        GetStat(stat).name = stat.ToString(); //Renames element name in Unity Inspector
    }

    public override Stat GetStat(WEAPONSTATS stat) => weaponStats[(int)stat];
}