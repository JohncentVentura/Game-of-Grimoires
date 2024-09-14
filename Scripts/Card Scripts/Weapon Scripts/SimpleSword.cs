using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSword : Weapon
{
    #region Overridden Methods
    protected override void OnEnable() => base.OnEnable();
    protected override void Start() => base.Start();
    protected override void FixedUpdate() => StateMachine(true);
    protected override void Update() => StateMachine(false);
    protected override void AnimEventResetState() => base.AnimEventResetState();
    #endregion
    
    #region State Machine
    protected override void StateMachine(bool usePhysics)
    {
        switch (animState)
        {
            case EAnimStates.Idle:
                IdleState(usePhysics);
                break;
            case EAnimStates.Attack:
                AttackState(usePhysics);
                break;
        }
    }

    public void IdleState(bool usePhysics)
    {
        if (usePhysics)
        {

        }
        else
        {
            animator.Play("IdleState");
        }
    }

    public void AttackState(bool usePhysics)
    {
        if (usePhysics)
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