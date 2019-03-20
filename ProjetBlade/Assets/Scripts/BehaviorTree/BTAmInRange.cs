﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The AmInRange node returns true when the AI is in range of target, otherwise it returns failure.
public class BTAmInRange : ABTNode {

    [HideInInspector]
    public GameObject target;
    [HideInInspector]
    public float range;

    public override TaskState Tick() {
        //Check if the target is within range.
        if (Vector3.Distance(transform.position, target.transform.position) <= range) {
            //Debug.Log("AmInRange || Succes");
            return TaskState.Succes;
        }
        else {
            //Debug.Log("AmInRange || Failure");
            return TaskState.Failure;
        }
    }

}
