using System;
using System.Collections.Generic;
using System.Numerics;

#region Stats Enums
[Serializable]
public enum PLAYERSTATS
{
    PlayerDirection,
    HitPoints, AttackDamage, AttackRange, AttackSpeed, MovementSpeed, //The same from WeaponStats
}

[Serializable]
public enum CARDSTATS
{
    Mana, Cooldown, Duration
}

[Serializable]
public enum CREATURESTATS
{
    HitPoints, AttackDamage, AttackRange, AttackSpeed, MovementSpeed
}

[Serializable]
public enum SPELLSTATS
{
    Damage, Heal
}

[Serializable]
public enum WEAPONSTATS
{
    HitPoints, AttackDamage, AttackRange, AttackSpeed, MovementSpeed
}
#endregion

#region Stats
[Serializable]
public class Stat
{
    public string name;
    public float maxValue;
    public float value;
    public float regeneration;

    public Stat(float maxValue)
    {
        this.maxValue = maxValue;
        value = this.maxValue;
    }
}
#endregion

#region Properties
[Serializable]
public enum CARDPROPS
{
    MainType, CardName, ManaType, SubType
}

[Serializable]
public class Property
{
    public string name;
    public string value;

    public Property(string value)
    {
        this.value = value;
    }
}
#endregion