using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    public void LoadData(GameData gameData);
    public void SaveData(GameData gameData);
}

/*
    public void LoadData(GameData gameData)
    {
        
    }

    public void SaveData(GameData gameData)
    {
        
    }
*/