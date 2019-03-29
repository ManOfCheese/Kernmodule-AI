using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTRoot : ABTNode {
    public BTRoot(List<ABTNode> childNodes, Blackboard blackBoard, bool isLeafNode, bool isRootNode) {
        this.childNodes = childNodes;
        this.blackBoard = blackBoard;
        this.isLeafNode = isLeafNode;
        this.isRootNode = isRootNode;
    }

    public void StartBT() {
        //If we have no nodes throw an error because then the tree won't work.
        if (childNodes == null) {
            Debug.LogError("BehaviorTree root has no nodes");
        }
        foreach (ABTNode node in childNodes) {
            node.Tick();
        }
    }
}
