using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableSettingsData
{   
    [Header("Inputs")]
    public List<string> movementInputKeys;
    public List<string> cardInputKeys;
    public string attackInputKey;
}

[CreateAssetMenu(fileName = "SettingsData", menuName = "ScriptableObjects/SettingsData")]
public class SettingsData : ScriptableObject
{   
    [Header("Inputs")]
    public List<string> movementInputKeys;
    public List<string> cardInputKeys;
    public string attackInputKey;

    public void InitSettingsData()
    {
        movementInputKeys = new List<string> { "Horizontal", "Vertical" };
        cardInputKeys = new List<string> { "Fire1", "Fire2", "Fire3", "Fire4" };
        attackInputKey = "Fire5";
    }
}