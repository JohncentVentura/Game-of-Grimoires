using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "PlayerBlazeBall", menuName = "ScriptableObjects/PlayerCards/PlayerBlazeBall")]
public class PlayerBlazeBall : SpellData
{
    public override void InitCardData()
    {
        base.InitCardData();
        AddProp(CARDPROPS.CardName, "Blaze Ball");
        AddProp(CARDPROPS.ManaType, GameManager.Instance.card.manaTypesDict[Card.MANATYPES.FIRE]);
        AddProp(CARDPROPS.SubType, "Damage");

        AddStat(CARDSTATS.Mana, 1);
        AddStat(CARDSTATS.Cooldown, 10);
        AddStat(CARDSTATS.Duration, 6);
    }
}