using System;
using System.Collections.Generic;
using UnityEngine;

/*
    CardData children contains the base value, override value by creating new CardData Asset
*/
#region Serializable CardData
[Serializable]
public class SerializableCardData
{
    public List<Prop> cardProps;
    public List<Stat> cardStats;
}

[Serializable]
public class SerializableCreatureData : SerializableCardData
{
    public List<Stat> creatureStats;

    public SerializableCreatureData()
    {
        cardProps = new List<Prop>();
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
        cardProps = new List<Prop>();
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
        cardProps = new List<Prop>();
        cardStats = new List<Stat>();
        weaponStats = new List<Stat>();
    }
}
#endregion

public class CardData : ScriptableObject
{
    public Sprite cardSprite;
    public List<Prop> cardProps;
    public List<Stat> cardStats;

    public void AddProp(ECardProps prop, string propName)
    {
        cardProps.Insert((int)prop, new Prop(propName));
        GetProp(prop).name = prop.ToString(); //Renames element name in Unity Inspector
    }

    public void AddStat(ECardStats stat, float maxValue)
    {
        cardStats.Insert((int)stat, new Stat(maxValue));
        GetStat(stat).name = stat.ToString(); //Renames element name in Unity Inspector
    }

    public Prop GetProp(ECardProps prop) => cardProps[(int)prop];
    public Stat GetStat(ECardStats stat) => cardStats[(int)stat];

    public virtual void InitCardData() { } //Override in CreatureData, SpellData, & WeaponData
    public virtual void AddStat(ECreatureStats stat, float maxValue) { } //Override in CreatureData
    public virtual void AddStat(ESpellStats stat, float maxValue) { } //Override in SpellData
    public virtual void AddStat(EWeaponStats stat, float maxValue) { } //Override in WeaponData
    public virtual Stat GetStat(ECreatureStats stat) => null; //Override in CreatureData
    public virtual Stat GetStat(ESpellStats stat) => null; //Override in SpellData
    public virtual Stat GetStat(EWeaponStats stat) => null; //Override in WeaponData
    public virtual CardObject GetCardObject() => null; //Override in childrens of CreatureData, SpellData, & WeaponData
    public virtual CardData GetNewCardData() => null; //Override in CreatureData, SpellData, & WeaponData
}

public class CreatureData : CardData
{
    public List<Stat> creatureStats;

    public override void InitCardData()
    {
        cardProps = new List<Prop>();
        AddProp(ECardProps.MainType, ObjectManager.Instance.mainTypes[ObjectManager.EMainTypes.Creature]);
        cardStats = new List<Stat>();
        creatureStats = new List<Stat>();
    }

    public override void AddStat(ECreatureStats stat, float maxValue)
    {
        creatureStats.Insert((int)stat, new Stat(maxValue));
        GetStat(stat).name = stat.ToString(); //Renames element name in Unity Inspector
    }

    public override Stat GetStat(ECreatureStats stat) => creatureStats[(int)stat];

    public override CardData GetNewCardData()
    {   
        CreatureData newCreatureData = Instantiate(this);
        return newCreatureData;
    }
}

public class SpellData : CardData
{
    public List<Stat> spellStats;

    public override void InitCardData()
    {
        cardProps = new List<Prop>();
        AddProp(ECardProps.MainType, ObjectManager.Instance.mainTypes[ObjectManager.EMainTypes.Spell]);
        cardStats = new List<Stat>();
        spellStats = new List<Stat>();
    }

    public override void AddStat(ESpellStats stat, float maxValue)
    {
        spellStats.Insert((int)stat, new Stat(maxValue));
        GetStat(stat).name = stat.ToString(); //Renames element name in Unity Inspector
    }

    public override Stat GetStat(ESpellStats stat) => spellStats[(int)stat];

    public override CardData GetNewCardData()
    {   
        SpellData newSpellData = Instantiate(this);
        return newSpellData;
    }
}

public class WeaponData : CardData
{
    public List<Stat> weaponStats;

    public override void InitCardData()
    {
        cardProps = new List<Prop>();
        AddProp(ECardProps.MainType, ObjectManager.Instance.mainTypes[ObjectManager.EMainTypes.Weapon]);
        cardStats = new List<Stat>();
        weaponStats = new List<Stat>();
    }

    public override void AddStat(EWeaponStats stat, float maxValue)
    {
        weaponStats.Insert((int)stat, new Stat(maxValue));
        GetStat(stat).name = stat.ToString(); //Renames element name in Unity Inspector
    }

    public override Stat GetStat(EWeaponStats stat) => weaponStats[(int)stat];

    public override CardData GetNewCardData()
    {   
        WeaponData newWeaponData = Instantiate(this);
        return newWeaponData;
    }
}