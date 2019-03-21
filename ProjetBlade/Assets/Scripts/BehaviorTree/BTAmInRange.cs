using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The AmInRange node returns true when the AI is in range of target, otherwise it returns failure.
public class BTAmInRange : ABTNode {
    public BTAmInRange(List<ABTNode> childNodes, BlackBoard blackBoard, bool isLeafNode, bool isRootNode) {
        this.childNodes = childNodes;
        this.blackBoard = blackBoard;
        this.isLeafNode = isLeafNode;
        this.isRootNode = isRootNode;
    }

    public override TaskState Tick() {
        //Check if the target is within range.
        if (Vector3.Distance(blackBoard.self.transform.position, blackBoard.target.transform.position) <= blackBoard.range) {
            //Debug.Log("AmInRange || Succes");
            return TaskState.Succes;
        }
        else {
            //Debug.Log("AmInRange || Failure");
            return TaskState.Failure;
        }
    }
}
