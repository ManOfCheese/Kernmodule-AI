using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The MoveTowardsTarget node will return running until it has reached its target when it will return succes. If it cannot reach the target it returns failure.
public class BTMoveTowardsTarget : ABTNode {

    public GameObject target;
    public int pathFound = 0;
    public bool goalReached = false;

    private AStarUnit unit;

    private void Start() {
        unit = GetComponent<AStarUnit>();
    }

    public override TaskState Tick() {
        //Reset checks.
        pathFound = 0;
        goalReached = false;

        unit.RequestPath(target.transform);
        //If the goal is reached and there is not not a path (i know that's confusing) return running.
        if (goalReached == false && pathFound != 2) {
            return TaskState.Running;
        }
        //If there is not a path return failure.
        else if (pathFound == 2) {
            return TaskState.Failure;
        }
        //If we reached our goal return succes.
        else if (goalReached == true) {
            return TaskState.Succes;
        }
        return TaskState.Failure;
    }

}
