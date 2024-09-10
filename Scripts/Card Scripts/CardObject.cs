using System.Collections.Generic;
using UnityEngine;

/*
    Using some OOP (virtual, override) causes bugs & warnings in Unity Animation Events
*/

public class CardObject : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    public enum EAnimStates
    {
        Idle, Attack
    }
    public EAnimStates animState;

    void OnEnable()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animState = EAnimStates.Idle;
    }
}

public class Creature : CardObject
{
    public CreatureData creatureData;
}

public class Spell : CardObject
{
    public SpellData spellData;
}

public class Weapon : CardObject
{
    public WeaponData weaponData;
}