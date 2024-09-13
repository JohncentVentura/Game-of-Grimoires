using System;
using System.Collections.Generic;
using System.Numerics;

#region Stat Enums
[Serializable]
public enum EPlayerStats
{
    PlayerDirection,
    HitPoints, AttackDamage, AttackRange, AttackSpeed, MovementSpeed, //The same from WeaponStats
}

[Serializable]
public enum ECardStats
{
    InstanceID, Mana, Cooldown, Duration
}

[Serializable]
public enum ECreatureStats
{
    HitPoints, AttackDamage, AttackRange, AttackSpeed, MovementSpeed
}

[Serializable]
public enum ESpellStats
{
    Damage, Heal
}

[Serializable]
public enum EWeaponStats
{
    HitPoints, AttackDamage, AttackRange, AttackSpeed, MovementSpeed
}
#endregion

#region Stat Class
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