﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The priority selector will process all children in sequence returning succes if any child succeeds not processing further children.
//Thus it will only return failure if all children fail.
public class BTPrioritySelector : ABTNode {
    public BTPrioritySelector(List<ABTNode> childNodes, Blackboard blackBoard, bool isLeafNode, bool isRootNode) {
        this.childNodes = childNodes;
        this.blackBoard = blackBoard;
        this.isLeafNode = isLeafNode;
        this.isRootNode = isRootNode;
    }

    public override TaskState Tick() {
        foreach (ABTNode node in childNodes) {
            //If any node succeeds return succes.
            if (node.Tick() == TaskState.Succes) {
                return TaskState.Succes;
            }
            //If any node is still running return runnig.
            else if (node.Tick() == TaskState.Running) {
                return TaskState.Running;
            }
        }
        return TaskState.Failure;
    }
}
