using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Creature
{   
    #region Overridden Methods
    protected override void OnEnable() => base.OnEnable();
    protected override void Start() => base.Start();
    protected override void AnimEventResetState() => base.AnimEventResetState();
    #endregion

    #region State Machine
    void FixedUpdate() => StateMachine(true);
    void Update() => StateMachine(false);

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