using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "PlayerBird", menuName = "ScriptableObjects/PlayerCards/PlayerBird")]
public class PlayerBird : CreatureData
{
    public override void InitCardData()
    {
        base.InitCardData();
        AddProp(CARDPROPS.CardName, "Bird");
        AddProp(CARDPROPS.ManaType, GameManager.Instance.card.manaTypesDict[Card.MANATYPES.EARTH]);
        AddProp(CARDPROPS.SubType, "Winged Beast");
        
        AddStat(CARDSTATS.Mana, 3);
        AddStat(CARDSTATS.Cooldown, 3);
        AddStat(CARDSTATS.Duration, 4);

        AddStat(CREATURESTATS.HitPoints, 10f);
        AddStat(CREATURESTATS.AttackDamage, 2f);
        AddStat(CREATURESTATS.AttackRange, 1f);
        AddStat(CREATURESTATS.AttackSpeed, 1f);
        AddStat(CREATURESTATS.MovementSpeed, 50f);
    }
}