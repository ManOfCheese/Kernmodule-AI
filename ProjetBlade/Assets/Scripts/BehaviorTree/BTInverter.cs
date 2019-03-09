using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the inverter returns the reverse of it's childNode.
public class BTInverter : ABTNode {

    public override TaskState Tick() {
        //If the child is succesful return failure.
        if (childNode.Tick() == TaskState.Succes) {
            return TaskState.Failure;
        }
        //If the child fails return succes.
        else if (childNode.Tick() == TaskState.Failure) {
            return TaskState.Succes;
        }
        else {
            return childNode.Tick();
        }
    }

}
