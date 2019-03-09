using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The AmSeen node will return succes if target is looking at it and failure if not.
public class BTAmSeen : ABTNode {

    public GameObject target;
    public float angle;

    public override TaskState Tick() {
        if (Vector3.Angle(target.transform.forward, transform.position - target.transform.position) < angle) {
            //Debug.Log("AmSeen || Succes");
            return TaskState.Succes;
        }
        else {
            //Debug.Log("AmSeen || Failure");
            return TaskState.Failure;
        }
    }

}
