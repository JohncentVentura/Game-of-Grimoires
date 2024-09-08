using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    public enum ANIMSTATES
    {
        Idle, Attack
    }
    public ANIMSTATES animState;

    #region CardObject & CardData Attributes
    public CardData cardData;
    public List<Property> cardProps;
    public List<Stat> cardStats;
    #endregion

    public virtual void InitCardObject(CardData cardData)
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animState = ANIMSTATES.Idle;
        
        cardData.InitCardData();
        this.cardData = cardData;
        cardProps = new List<Property>();
        cardProps = cardData.cardProps;
        cardStats = new List<Stat>();
        cardStats = cardData.cardStats;
    }

    #region CardObject & CardData Methods
    public Property GetCardProp(CARDPROPS prop) => cardProps[(int)prop];
    public Stat GetCardStat(CARDSTATS stat) => cardStats[(int)stat];
    public virtual Stat GetCreatureStat(CREATURESTATS stat) => null; //Override in CreatureObject
    public virtual Stat GetSpellStat(SPELLSTATS stat) => null; //Override in SpellObject
    public virtual Stat GetWeaponStat(WEAPONSTATS stat) => null; //Override in WeaponObject
    public virtual List<Stat> GetStats() => null; //Override in CreatureObject, SpellObject, & WeaponObject
    #endregion
}

public class CreatureObject : CardObject
{
    public List<Stat> creatureStats;

    public override void InitCardObject(CardData cardData)
    {
        base.InitCardObject(cardData);
        creatureStats = new List<Stat>();
        creatureStats = cardData.GetStats();
    }

    public override Stat GetCreatureStat(CREATURESTATS stat) => creatureStats[(int)stat];
    public override List<Stat> GetStats() => creatureStats; 
}

public class SpellObject : CardObject
{
    public List<Stat> spellStats;

    public override void InitCardObject(CardData cardData)
    {
        base.InitCardObject(cardData);
        spellStats = new List<Stat>();
        spellStats = cardData.GetStats();
    }

    public override Stat GetSpellStat(SPELLSTATS stat) => spellStats[(int)stat];
    public override List<Stat> GetStats() => spellStats; 
}

public class WeaponObject : CardObject
{
    public List<Stat> weaponStats;

    public override void InitCardObject(CardData cardData)
    {
        base.InitCardObject(cardData);
        weaponStats = new List<Stat>();
        weaponStats = cardData.GetStats();
    }

    public override Stat GetWeaponStat(WEAPONSTATS stat) => weaponStats[(int)stat];
    public override List<Stat> GetStats() => weaponStats; 
}