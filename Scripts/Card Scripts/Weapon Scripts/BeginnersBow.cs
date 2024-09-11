using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginnersBow : Weapon
{   
    void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Blend", 1);
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animState = EAnimStates.Idle;
    }
    
    void Start() => weaponData = (WeaponData)weaponData.GetNewCardData();
    
    void FixedUpdate() => StateMachine(true);

    void Update()
    {
        StateMachine(false);
    }

    #region StateMachine
    public void StateMachine(bool isUsingPhysics)
    {
        switch (animState)
        {
            case EAnimStates.Idle:
                IdleState(isUsingPhysics);
                break;
            case EAnimStates.Attack:
                AttackState(isUsingPhysics);
                break;
        }
    }

    public void AnimEventResetState() //Called as an event in animation
    {
        animState = EAnimStates.Idle;
        transform.rotation = Quaternion.identity; //For Bow-type & Staff-type Weapons
    }

    public void IdleState(bool isUsingPhysics)
    {
        if (isUsingPhysics)
        {

        }
        else
        {
            animator.Play("IdleState");
        }
    }

    public void AttackState(bool isUsingPhysics)
    {
        if (isUsingPhysics)
        {

        }
        else
        {
            //animator.speed = GetStat(WEAPONSTATS.AttackSpeed).currValue;
            animator.Play("AttackState");
        }
    }
    #endregion
}