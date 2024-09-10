using UnityEngine;

[CreateAssetMenu(fileName = "BeginnersBowData", menuName = "ScriptableObjects/WeaponCard/BeginnersBowData")]
public class BeginnersBowData : WeaponData
{
    public override void InitCardData()
    {
        base.InitCardData();
        AddProp(ECardProps.CardName, "Beginners Bow");
        AddProp(ECardProps.ManaType, ObjectManager.Instance.manaTypes[ObjectManager.EManaTypes.Earth]);
        AddProp(ECardProps.SubType, ObjectManager.Instance.weaponTypes[ObjectManager.EWeaponTypes.Bow]);
        
        AddStat(ECardStats.Mana, 4);
        AddStat(ECardStats.Cooldown, 4);
        AddStat(ECardStats.Duration, 14);

        AddStat(EWeaponStats.HitPoints, 0f);
        AddStat(EWeaponStats.AttackDamage, 4f);
        AddStat(EWeaponStats.AttackRange, 1.2f);
        AddStat(EWeaponStats.AttackSpeed, 2f);
        AddStat(EWeaponStats.MovementSpeed, 5f);
    }

    public override CardObject GetCardObject()
    {
        return ObjectManager.Instance.beginnersBow;
    }
}