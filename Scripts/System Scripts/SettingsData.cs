using UnityEngine;

[System.Serializable]
public class SerializableSettingsData
{   
    [Header("Inputs")]
    public string[] movementInputKeys;
    public string[] cardInputKeys;
    public string attackInputKey;
}

[CreateAssetMenu(fileName = "SettingsData", menuName = "ScriptableObjects/SettingsData")]
public class SettingsData : ScriptableObject
{   
    [Header("Inputs")]
    public string[] movementInputKeys;
    public string[] cardInputKeys;
    public string attackInputKey;

    public void InitSettingsData()
    {
        movementInputKeys = new string[] { "Horizontal", "Vertical" };
        cardInputKeys = new string[] { "Fire1", "Fire2", "Fire3", "Fire4" };
        attackInputKey = "Fire5";
    }
}