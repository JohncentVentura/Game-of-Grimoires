using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginnersBow : WeaponObject
{
    void Update() => StateMachine(false);
    void FixedUpdate() => StateMachine(true);

    #region StateMachine
    public void StateMachine(bool isUsingPhysics)
    {
        switch (animState)
        {
            case ANIMSTATES.Idle:
                IdleState(isUsingPhysics);
                break;
            case ANIMSTATES.Attack:
                AttackState(isUsingPhysics);
                break;
        }
    }

    public void AnimEventResetState() //Called as an event in animation
    {
        animState = ANIMSTATES.Idle;
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
