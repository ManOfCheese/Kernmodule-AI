using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State, IState {

    public float speed;

    public Animator animator;
    public GameObject player;
    public Grid grid;
    private Node targetNode;

    public override void EnterState() {
        animator.SetTrigger("Standard");
        grid.FindPath(player.transform.position);
    }

    public override void ExitState() {

    }

    public override void UpdateState() {
        targetNode = grid.GetNextNode(transform.position);
        Debug.Log(targetNode);
        Vector3.MoveTowards(transform.position, targetNode.worldPos, speed * Time.deltaTime);
    }
}
