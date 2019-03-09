using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The MoveTowardsTarget node will return running until it has reached its target when it will return succes. If it cannot reach the target it returns failure.
public class BTMoveTowardsTarget : ABTNode {

    public GameObject target;
    public int pathFound = 0;
    public bool goalReached = false;
    public bool initialized;
    public AStarUnit unit;
    public Animator animator;

    public void Initialize() {
        //Reset checks.
        pathFound = 0;
        goalReached = false;
        unit.RequestPath(target.transform);
        animator.SetBool("Running", true);
    }

    public override TaskState Tick() {
        if (!initialized) {
            Initialize();
            initialized = true;
        }
        //If the goal is reached and there is not not a path (i know that's confusing) return running.
        if (goalReached == false && pathFound != 2) {
            //Debug.Log("MoveTowardsTarget || Running");
            return TaskState.Running;
        }
        //If there is not a path return failure.
        else if (pathFound == 2) {
            //Debug.Log("MoveTowardsTarget || Failure");
            animator.SetBool("Running", false);
            return TaskState.Failure;
        }
        //If we reached our goal return succes.
        else if (goalReached == true) {
            //Debug.Log("MoveTowardsTarget || Succes");
            initialized = false;
            animator.SetBool("Running", false);
            return TaskState.Succes;
        }
        //Debug.Log("MoveTowardsTarget || Nothing");
        initialized = false;
        animator.SetBool("Running", false);
        return TaskState.Failure;
    }

}
