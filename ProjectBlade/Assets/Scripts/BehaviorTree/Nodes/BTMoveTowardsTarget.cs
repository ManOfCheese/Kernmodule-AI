using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The MoveTowardsTarget node will return running until it has reached its target when it will return succes. If it cannot reach the target it returns failure.
public class BTMoveTowardsTarget : ABTNode {
    public int pathFound = 0;
    public bool goalReached = false;
    public bool initialized;

    private string targetKeyword;

    public BTMoveTowardsTarget(List<ABTNode> childNodes, Blackboard blackBoard, bool isLeafNode, bool isRootNode, string targetKeyword) {
        this.childNodes = childNodes;
        this.blackBoard = blackBoard;
        this.isLeafNode = isLeafNode;
        this.isRootNode = isRootNode;
        this.targetKeyword = targetKeyword;
    }

    public void Initialize() {
        //Reset checks.
        pathFound = 0;
        goalReached = false;
        blackBoard.unit.RequestPath(blackBoard.target.transform);
        blackBoard.animator.SetBool("Running", true);
    }

    public override TaskState Tick() {
        blackBoard.SetTarget(targetKeyword);

        if (!initialized) {
            Initialize();
            initialized = true;
        }
        //If the goal is reached and there is not not a path (i know that's confusing) return running.
        if (goalReached == false && pathFound != 2) {
            return TaskState.Running;
        }
        //If there is not a path return failure.
        else if (pathFound == 2) {
            blackBoard.animator.SetBool("Running", false);
            initialized = false;
            return TaskState.Failure;
        }
        //If we reached our goal return succes.
        else if (goalReached == true) {
            initialized = false;
            blackBoard.animator.SetBool("Running", false);
            return TaskState.Succes;
        }
        initialized = false;
        blackBoard.animator.SetBool("Running", false);
        return TaskState.Failure;
    }

}
