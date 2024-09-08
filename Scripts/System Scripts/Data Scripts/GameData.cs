using System;
using UnityEngine;

[Serializable]
public class GameData
{   
    public SerializableSettingsData settingsData;
    public SerializablePlayerData playerData;

    #region Creature Cards
    //[Header("Creature Cards")]
    #endregion

    #region Spell Cards
    //[Header("Spell Cards")]
    #endregion

    #region Weapon Cards
    //[Header("Weapon Cards")]
    #endregion

    public GameData()
    {   
        settingsData = new SerializableSettingsData();
        playerData = new SerializablePlayerData();

        NewCreatureCardsData();
        NewSpellCardsData();
        NewWeaponCardsData();
    }
    
    public void NewCreatureCardsData()
    {
        //birdCard = new SerializableCreatureCard();
    }

    public void NewSpellCardsData()
    {
        //blazeBallCard = new SerializableSpellCard();
    }

    public void NewWeaponCardsData()
    {
        //simpleSwordCard = new SerializableWeaponCard();
        //beginnersBowCard = new SerializableWeaponCard();
    }
}