using System;
using System.Collections.Generic;
using UnityEngine;

/*
    NOTE: Creating a virtual method needs to be overridden in children, or else Animation Event will cause bugs & errors
*/

public abstract class CardObject : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Hitbox hitbox;
    [HideInInspector] public Hurtbox hurtbox;
    public enum EAnimStates
    {
        Idle, Move, Attack
    }
    public EAnimStates animState;
}

public abstract class Creature : CardObject
{
    public CreatureData creatureData;

    protected virtual void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Blend", 1);
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animState = EAnimStates.Idle;

        hitbox = transform.GetChild(0).GetComponent<Hitbox>();
        hurtbox = transform.GetChild(1).GetComponent<Hurtbox>();
    }

    protected virtual void Start()
    {
        creatureData = (CreatureData)creatureData.GetNewCardData();
        //TODO: creatureObj.tag = cardData.tag
    }

    protected virtual void FixedUpdate() => StateMachine(true);
    protected virtual void Update() => StateMachine(false);
    protected virtual void StateMachine(bool usePhysics){}

    protected virtual void AnimEventResetState() //Called as an event in animation
    {
        animState = EAnimStates.Idle;
    }
}

public abstract class Spell : CardObject
{
    public SpellData spellData;

    protected virtual void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Blend", 1);
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animState = EAnimStates.Idle;
    }
    
    protected virtual void Start() 
    {
        spellData = (SpellData)spellData.GetNewCardData();
    }

    protected virtual void FixedUpdate() => StateMachine(true);
    protected virtual void Update() => StateMachine(false);
    protected virtual void StateMachine(bool usePhysics){}

    protected virtual void AnimEventResetState() //Called as an event in animation
    {
        animState = EAnimStates.Idle;
    }
}

public abstract class Weapon : CardObject
{
    public WeaponData weaponData;

    protected virtual void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Blend", 1);
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animState = EAnimStates.Idle;
    }

    protected virtual void Start()
    {
        weaponData = (WeaponData)weaponData.GetNewCardData();
    }

    protected virtual void FixedUpdate() => StateMachine(true);
    protected virtual void Update() => StateMachine(false);
    protected virtual void StateMachine(bool usePhysics){}
    
    protected virtual void AnimEventResetState() //Called as an event in animation
    {
        animState = EAnimStates.Idle;
        transform.rotation = Quaternion.identity; //For Bow-type & Staff-type Weapons
    }
}