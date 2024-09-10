using UnityEngine;

[CreateAssetMenu(fileName = "SimpleSwordData", menuName = "ScriptableObjects/WeaponCard/SimpleSwordData")]
public class SimpleSwordData : WeaponData
{
    public override void InitCardData()
    {
        base.InitCardData();
        AddProp(ECardProps.CardName, "Beginners Bow");
        AddProp(ECardProps.ManaType, ObjectManager.Instance.manaTypes[ObjectManager.EManaTypes.Fire]);
        AddProp(ECardProps.SubType, ObjectManager.Instance.weaponTypes[ObjectManager.EWeaponTypes.Sword]);
        
        AddStat(ECardStats.Mana, 3);
        AddStat(ECardStats.Cooldown, 3);
        AddStat(ECardStats.Duration, 13);

        AddStat(EWeaponStats.HitPoints, 0f);
        AddStat(EWeaponStats.AttackDamage, 3f);
        AddStat(EWeaponStats.AttackRange, 1f);
        AddStat(EWeaponStats.AttackSpeed, 1f);
        AddStat(EWeaponStats.MovementSpeed, 0f);
    }

    public override CardObject GetCardObject()
    {
        return ObjectManager.Instance.simpleSword;
    }
}