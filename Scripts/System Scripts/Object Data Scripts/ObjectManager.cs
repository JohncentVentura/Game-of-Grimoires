using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
   public static ObjectManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #region Cards
    public Sprite[] manaTypeSprites;
    
    public enum EMainTypes
    {
        Creature, Spell, Weapon,
    }
    public readonly Dictionary<EMainTypes, string> mainTypes = new()
    {
        { EMainTypes.Creature, "Creature" }, { EMainTypes.Spell, "Spell" }, { EMainTypes.Weapon, "Weapon" }
    };

    public enum EManaTypes
    {
        Aether, Earth, Fire, Nether, Water
    }
    public readonly Dictionary<EManaTypes, string> manaTypes = new()
    {
        {EManaTypes.Aether, "Aether" }, {EManaTypes.Earth, "Earth" }, {EManaTypes.Fire, "Fire" },
        {EManaTypes.Nether, "Nether" }, {EManaTypes.Water, "Water"}
    };

    public enum ECreatureTypes
    {

    }
    public readonly Dictionary<ECreatureTypes, string> creatureTypes = new()
    {

    };

    public enum ESpellTypes
    {

    }
    public readonly Dictionary<ESpellTypes, string> spellTypes = new()
    {

    };

    public enum EWeaponTypes
    {
        Sword, Heavy, Polearm, Bow, Staff
    }
    public readonly Dictionary<EWeaponTypes, string> weaponTypes = new()
    {
        { EWeaponTypes.Sword, "Sword" }, { EWeaponTypes.Heavy, "Heavy" }, { EWeaponTypes.Polearm, "Polearm" },
        { EWeaponTypes.Bow, "Bow" }, { EWeaponTypes.Staff, "Staff" }
    };
    #endregion
    
    #region Creatures
    [Header("Creatures")]
    public Creature bird;
    #endregion

    #region Spells
    [Header("Spells")]
    public Spell blazeBall;
    #endregion

    #region Weapons
    [Header("Weapons")]
    public Weapon beginnersBow;
    public Weapon simpleSword;
    #endregion
}
