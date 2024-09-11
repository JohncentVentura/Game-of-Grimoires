using System;
using System.Collections.Generic;
using UnityEngine;

/*
    Cannot create methods in parent because it causes bugs in Animation Event, create methods in children instead
*/

public class CardObject : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    public enum EAnimStates
    {
        Idle, Move, Attack
    }
    public EAnimStates animState;
}

public class Creature : CardObject
{
    public CreatureData creatureData;

    /*Template for Creature children

    */
}

public class Spell : CardObject
{
    public SpellData spellData;

    /*Template for Spell children

    */
}

public class Weapon : CardObject
{
    public WeaponData weaponData;

    /*Template for Weapon children

    */
}

