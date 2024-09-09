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
    public enum ANIMSTATES
    {
        Idle, Attack
    }
    public ANIMSTATES animState;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animState = ANIMSTATES.Idle;
    }
}

public class CreatureObject : CardObject
{
    public CreatureData creatureData;
}

public class SpellObject : CardObject
{
    public SpellData spellData;
}

public class WeaponObject : CardObject
{
    public WeaponData weaponData;
}