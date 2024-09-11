using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Cards")]
    public CreatureData birdData;
    public SpellData blazeBallData;
    public WeaponData beginnersBowData;
    public WeaponData simpleSwordData;

    public void InitEnemyData()
    {
        //Cards
        birdData.InitCardData();
        //blazeBallData.InitCardData();
        //beginnersBowData.InitCardData();
        //simpleSwordData.InitCardData();
    }
}