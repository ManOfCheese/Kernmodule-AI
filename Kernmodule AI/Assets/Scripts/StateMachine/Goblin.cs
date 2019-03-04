using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Goblin : MonoBehaviour {

    public GameObject player;

    public StateMachine stateMachine;
    public State idleState;
    public State chaseState;
    public State meleeAttackState;
    public State rangedAttackState;

    public float meleeRange;
    public float chaseRange;

    private void Update() {
        if (Vector3.Distance(transform.position, player.transform.position) > meleeRange && Vector3.Distance(transform.position, player.transform.position) < chaseRange) {
            stateMachine.ChangeState(chaseState);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) < meleeRange) {
            stateMachine.ChangeState(meleeAttackState);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > chaseRange) {
            stateMachine.ChangeState(rangedAttackState);
        }
        else {
            stateMachine.ChangeState(idleState);
        }
    }

}
