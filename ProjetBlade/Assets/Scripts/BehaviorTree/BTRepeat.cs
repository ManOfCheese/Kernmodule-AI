using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The repeat node will retick it's child node every time it returns a result.
public class BTRepeat : ABTNode {
    public BTRepeat(List<ABTNode> childNodes, BlackBoard blackBoard, bool isLeafNode, bool isRootNode) {
        this.childNodes = childNodes;
        this.blackBoard = blackBoard;
        this.isLeafNode = isLeafNode;
        this.isRootNode = isRootNode;
    }

    public override TaskState Tick() {
        foreach (ABTNode node in childNodes) {
            if (node.Tick() == TaskState.Failure || node.Tick() == TaskState.Succes || node.Tick() == TaskState.Running) {
                Tick();
            }
        }
        Debug.Log("Sequence || Succes");
        return TaskState.Succes;
    }
}
