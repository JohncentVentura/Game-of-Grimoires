using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginnersBow : WeaponObject
{
    private void Update() => StateMachine(false);
    private void FixedUpdate() => StateMachine(true);

    #region ANIMSTATES
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

    public virtual void IdleState(bool isUsingPhysics)
    {
        if (isUsingPhysics)
        {

        }
        else
        {
            animator.Play("IdleState");
        }
    }

    public virtual void AttackState(bool isUsingPhysics)
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
