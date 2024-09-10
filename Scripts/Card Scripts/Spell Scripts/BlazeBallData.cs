using UnityEngine;

[CreateAssetMenu(fileName = "BlazeBallData", menuName = "ScriptableObjects/SpellCard/BlazeBallData")]
public class BlazeBallData : SpellData
{
    public override void InitCardData()
    {
        base.InitCardData();
        AddProp(ECardProps.CardName, "Blaze Ball");
        AddProp(ECardProps.ManaType, ObjectManager.Instance.manaTypes[ObjectManager.EManaTypes.Fire]);
        AddProp(ECardProps.SubType, "Damage");

        AddStat(ECardStats.Mana, 1);
        AddStat(ECardStats.Cooldown, 10);
        AddStat(ECardStats.Duration, 6);
    }

    public override CardObject GetCardObject()
    {
        return ObjectManager.Instance.blazeBall;
    }
}