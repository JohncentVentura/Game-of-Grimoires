using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "PlayerBeginnersBow", menuName = "ScriptableObjects/PlayerCards/PlayerBeginnersBow")]
public class PlayerBeginnersBow : WeaponData
{
    public override void InitCardData()
    {
        base.InitCardData();
        AddProp(CARDPROPS.CardName, "Beginners Bow");
        AddProp(CARDPROPS.ManaType, GameManager.Instance.card.manaTypesDict[Card.MANATYPES.EARTH]);
        AddProp(CARDPROPS.SubType, GameManager.Instance.card.weaponTypeDict[Card.WEAPONTYPES.BOW]);
        
        AddStat(CARDSTATS.Mana, 4);
        AddStat(CARDSTATS.Cooldown, 4);
        AddStat(CARDSTATS.Duration, 14);

        AddStat(WEAPONSTATS.HitPoints, 0f);
        AddStat(WEAPONSTATS.AttackDamage, 4f);
        AddStat(WEAPONSTATS.AttackRange, 1.2f);
        AddStat(WEAPONSTATS.AttackSpeed, 2f);
        AddStat(WEAPONSTATS.MovementSpeed, 5f);
    }
}