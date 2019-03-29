using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The sequence node will run through all its children returning failure if any of them fails, and running if any of them is still running.
//Only when all child nodes of the sequence have succeeded does it returns succes.
public class BTSequence : ABTNode {
    public BTSequence(List<ABTNode> childNodes, Blackboard blackBoard, bool isLeafNode, bool isRootNode) {
        this.childNodes = childNodes;
        this.blackBoard = blackBoard;
        this.isLeafNode = isLeafNode;
        this.isRootNode = isRootNode;
    }

    public override TaskState Tick() {
        int succesCount = 0;
        //Tick all child nodes.
        foreach (ABTNode node in childNodes) {
            if (node.Tick() == TaskState.Failure) {
                return TaskState.Failure;
            }
            else if (node.Tick() == TaskState.Running) {
                return TaskState.Running;
            }
            else if (node.Tick() == TaskState.Succes) {
                succesCount++;
            }
        }
        //If all nodes succeeded return succes.
        if (succesCount == childNodes.Count) {
            return TaskState.Succes;
        }
        else {
            return TaskState.Failure;
        }
    }
}
