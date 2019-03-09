using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The AmInRange node returns true when the AI is in range of target, otherwise it returns failure.
public class BTAmInRange : ABTNode {

    public GameObject target;
    public float range;

    public override TaskState Tick() {
        //Check if the target is within range.
        if (Vector3.Distance(this.transform.position, target.transform.position) <= range) {
            return TaskState.Succes;
        }
        else {
            return TaskState.Failure;
        }
    }

}
