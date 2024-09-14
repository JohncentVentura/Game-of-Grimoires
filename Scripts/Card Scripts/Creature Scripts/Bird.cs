using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Creature
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
            case EAnimStates.Move:
                MoveState(usePhysics);
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
            animator.Play("IdleAndMoveState");
        }
    }

    public void MoveState(bool usePhysics)
    {
        if (usePhysics)
        {

        }
        else
        {
            animator.Play("IdleAndMoveState");
        }
    }

    public void AttackState(bool usePhysics)
    {
        if (usePhysics)
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
        if (other.gameObject.CompareTag(ObjectManager.Instance.tags[ObjectManager.ETags.Player]))
        {
            Debug.Log("Player alert");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(ObjectManager.Instance.tags[ObjectManager.ETags.Player]))
        {
            Debug.Log("Player seen");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(ObjectManager.Instance.tags[ObjectManager.ETags.Player]))
        {
            Debug.Log("Player left");
        }
    }

}