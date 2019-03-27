using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTRoot : ABTNode {
    public ABTNode runningNode;

    public BTRoot(List<ABTNode> childNodes, Blackboard blackBoard, bool isLeafNode, bool isRootNode) {
        this.childNodes = childNodes;
        this.blackBoard = blackBoard;
        this.isLeafNode = isLeafNode;
        this.isRootNode = isRootNode;
    }

    public void StartBT() {
        if (childNodes == null) {
            Debug.LogError("BehaviorTree root has no nodes");
        }

        if (runningNode == null) {
            foreach (ABTNode node in childNodes) {
                node.Tick();
            }
        }
        else {
            if (runningNode.Tick() == TaskState.Succes || runningNode.Tick() == TaskState.Failure) {
                runningNode = null;
            }
        }
    }
}
