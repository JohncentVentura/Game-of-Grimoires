using System;
using UnityEngine;

[Serializable]
public class GameData
{   
    public SerializableSettingsData settingsData;
    public SerializablePlayerData playerData;

    public GameData()
    {   
        settingsData = new SerializableSettingsData();
        playerData = new SerializablePlayerData();
    }
}