using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the inverter returns the reverse of it's childNode.
public class BTInverter : ABTNode {
    public BTInverter(List<ABTNode> childNodes, Blackboard blackBoard, bool isLeafNode, bool isRootNode) {
        this.childNodes = childNodes;
        this.blackBoard = blackBoard;
        this.isLeafNode = isLeafNode;
        this.isRootNode = isRootNode;
    }

    public override TaskState Tick() {
        //If the child is succesful return failure.
        foreach (ABTNode node in childNodes) {
            if (node.Tick() == TaskState.Succes) {
                //Debug.Log("Inverter || Failure");
                taskState = TaskState.Failure;
            }
            //If the child fails return succes.
            else if (node.Tick() == TaskState.Failure) {
                //Debug.Log("Inverter || Succes");
                taskState = TaskState.Succes;
            }
            else {
                taskState = node.Tick();
            }
        }
        return taskState;
    }
}
