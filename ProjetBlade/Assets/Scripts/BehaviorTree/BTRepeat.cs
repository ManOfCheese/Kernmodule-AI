using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The repeat node will retick it's child node every time it returns a result.
public class BTRepeat : ABTNode {

    private void Start() {
        Tick();
    }

    public override TaskState Tick() {
        if (childNode.Tick() == TaskState.Failure || childNode.Tick() == TaskState.Succes || childNode.Tick() == TaskState.Running) {
            Tick();
        }
        return TaskState.Succes;
    }

}
