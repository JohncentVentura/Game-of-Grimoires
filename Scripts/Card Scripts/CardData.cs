using System.Collections.Generic;
using UnityEngine;

public class CardData : ScriptableObject
{   
    #region CardObject & CardData Attributes
    public List<Property> cardProps;
    public List<Stat> cardStats;
    #endregion

    public virtual void InitCardData() { } //Override in CreatureData, SpellData, & WeaponData

    public void AddProp(CARDPROPS prop, string propName)
    {
        cardProps.Insert((int)prop, new Property(propName));
        GetCardProp(prop).name = prop.ToString();
    }

    public void AddStat(CARDSTATS stat, float maxValue)
    {
        cardStats.Insert((int)stat, new Stat(maxValue));
        GetCardStat(stat).name = stat.ToString();
    }

    public virtual void AddStat(CREATURESTATS stat, float maxValue) { } //Override in CreatureData
    public virtual void AddStat(SPELLSTATS stat, float maxValue) { } //Override in SpellData
    public virtual void AddStat(WEAPONSTATS stat, float maxValue) { } //Override in WeaponData
    
    #region CardObject & CardData Methods
    public Property GetCardProp(CARDPROPS prop) => cardProps[(int)prop];
    public Stat GetCardStat(CARDSTATS stat) => cardStats[(int)stat];
    public virtual Stat GetCreatureStat(CREATURESTATS stat) => null; //Override in CreatureData
    public virtual Stat GetSpellStat(SPELLSTATS stat) => null; //Override in SpellData
    public virtual Stat GetWeaponStat(WEAPONSTATS stat) => null; //Override in WeaponData
    public virtual List<Stat> GetStats() => null; //Override in CreatureData, SpellData, & WeaponData
    #endregion
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
        GetCreatureStat(stat).name = stat.ToString();
    }

    public override Stat GetCreatureStat(CREATURESTATS stat) => creatureStats[(int)stat];
    public override List<Stat> GetStats() => creatureStats;
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
        GetSpellStat(stat).name = stat.ToString();
    }

    public override Stat GetSpellStat(SPELLSTATS stat) => spellStats[(int)stat];
    public override List<Stat> GetStats() => spellStats;
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
        GetWeaponStat(stat).name = stat.ToString();
    }

    public override Stat GetWeaponStat(WEAPONSTATS stat) => weaponStats[(int)stat];
    public override List<Stat> GetStats() => weaponStats;
}