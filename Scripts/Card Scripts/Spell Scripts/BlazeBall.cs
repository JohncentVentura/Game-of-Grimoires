using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlazeBall : Spell
{
    #region Overridden Methods
    protected override void OnEnable() => base.OnEnable();
    protected override void Start() => base.Start();
    protected override void FixedUpdate() => StateMachine(true);
    protected override void Update() => StateMachine(false);
    protected override void AnimEventResetState() => base.AnimEventResetState();
    #endregion
}