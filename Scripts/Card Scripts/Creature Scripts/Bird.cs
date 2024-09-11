using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Creature
{
    void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Blend", 1);
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animState = EAnimStates.Idle;
    }

    void Start() => creatureData = (CreatureData)creatureData.GetNewCardData();

    void FixedUpdate() => StateMachine(true);

    void Update()
    {
        StateMachine(false);
    }

    #region State Machine
    public void StateMachine(bool isUsingPhysics)
    {
        switch (animState)
        {
            case EAnimStates.Idle:
                IdleState(isUsingPhysics);
                break;
            case EAnimStates.Move:
                MoveState(isUsingPhysics);
                break;
            case EAnimStates.Attack:
                AttackState(isUsingPhysics);
                break;
        }
    }

    public void AnimEventResetState() //Called as an event in animation
    {
        animState = EAnimStates.Idle;
    }

    public void IdleState(bool isUsingPhysics)
    {
        if (isUsingPhysics)
        {

        }
        else
        {
            animator.Play("IdleAndMoveState");
        }
    }

    public void MoveState(bool isUsingPhysics)
    {
        if (isUsingPhysics)
        {

        }
        else
        {
            animator.Play("IdleAndMoveState");
        }
    }

    public void AttackState(bool isUsingPhysics)
    {
        if (isUsingPhysics)
        {

        }
        else
        {
            animator.Play("AttackState");
        }
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {

    }

}