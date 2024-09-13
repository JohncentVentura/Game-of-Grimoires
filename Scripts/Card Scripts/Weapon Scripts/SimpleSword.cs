using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSword : Weapon
{
    #region Overridden Methods
    protected override void OnEnable() => base.OnEnable();
    protected override void Start() => base.Start();
    protected override void AnimEventResetState() => base.AnimEventResetState();
    #endregion

    void FixedUpdate() => StateMachine(true);
    void Update() => StateMachine(false);
    
    #region State Machine
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