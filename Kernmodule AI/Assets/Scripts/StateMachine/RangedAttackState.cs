using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedAttackState : State, IState {

    public Animator animator;
    public NavMeshAgent agent;

    public override void EnterState() {
        animator.SetTrigger("RangedAttack");
        //agent.isStopped = true;
        ExitState();
    }

    public override void ExitState() {

    }

    public override void UpdateState() {

    }
}
