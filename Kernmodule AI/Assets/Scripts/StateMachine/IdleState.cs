using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State, IState {

    public Animator animator;

    public override void EnterState() {
        animator.SetTrigger("Standard");
    }

    public override void ExitState() {

    }

    public override void UpdateState() {

    }
}