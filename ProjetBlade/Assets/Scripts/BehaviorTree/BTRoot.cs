using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTRoot : ABTNode {
    public BTRoot(List<ABTNode> childNodes, BlackBoard blackBoard, bool isLeafNode, bool isRootNode) {
        this.childNodes = childNodes;
        this.blackBoard = blackBoard;
        this.isLeafNode = isLeafNode;
        this.isRootNode = isRootNode;
    }

    public void StartBT() {
        foreach (ABTNode node in childNodes) {
            node.Tick();
        }
    }
}
