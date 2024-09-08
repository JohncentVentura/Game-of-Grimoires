using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "PlayerSimpleSword", menuName = "ScriptableObjects/PlayerCards/PlayerSimpleSword")]
public class PlayerSimpleSword : WeaponData
{
    public override void InitCardData()
    {
        base.InitCardData();
        AddProp(CARDPROPS.CardName, "Beginners Bow");
        AddProp(CARDPROPS.ManaType, GameManager.Instance.card.manaTypesDict[Card.MANATYPES.FIRE]);
        AddProp(CARDPROPS.SubType, GameManager.Instance.card.weaponTypeDict[Card.WEAPONTYPES.SWORD]);
        
        AddStat(CARDSTATS.Mana, 3);
        AddStat(CARDSTATS.Cooldown, 3);
        AddStat(CARDSTATS.Duration, 13);

        AddStat(WEAPONSTATS.HitPoints, 0f);
        AddStat(WEAPONSTATS.AttackDamage, 3f);
        AddStat(WEAPONSTATS.AttackRange, 1f);
        AddStat(WEAPONSTATS.AttackSpeed, 1f);
        AddStat(WEAPONSTATS.MovementSpeed, 0f);
    }
}