using UnityEngine;

[CreateAssetMenu(fileName = "BirdData", menuName = "ScriptableObjects/CreatureCard/BirdData")]
public class BirdData : CreatureData
{
    public override void InitCardData()
    {
        base.InitCardData();
        AddProp(ECardProps.CardName, "Bird");
        AddProp(ECardProps.ManaType, ObjectManager.Instance.manaTypes[ObjectManager.EManaTypes.Earth]);
        AddProp(ECardProps.SubType, "Winged Beast");
        
        AddStat(ECardStats.Mana, 3);
        AddStat(ECardStats.Cooldown, 3);
        AddStat(ECardStats.Duration, 4);

        AddStat(ECreatureStats.HitPoints, 10f);
        AddStat(ECreatureStats.AttackDamage, 2f);
        AddStat(ECreatureStats.AttackRange, 1f);
        AddStat(ECreatureStats.AttackSpeed, 1f);
        AddStat(ECreatureStats.MovementSpeed, 50f);
    }

    public override CardObject GetCardObject()
    {
        return ObjectManager.Instance.bird;
    }
}