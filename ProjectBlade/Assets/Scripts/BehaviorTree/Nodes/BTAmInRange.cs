using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The AmInRange node returns true when the AI is in range of target, otherwise it returns failure.
public class BTAmInRange : ABTNode {
    //These keywords determines which range and target we want to use in making our checks. They are passed into the constructor from the blackboard.
    private string rangeKeyword;
    private string targetKeyword;

    public BTAmInRange(List<ABTNode> childNodes, Blackboard blackBoard, bool isLeafNode, bool isRootNode, string targetKeyword, string rangeKeyword) {
        this.childNodes = childNodes;
        this.blackBoard = blackBoard;
        this.isLeafNode = isLeafNode;
        this.isRootNode = isRootNode;
        this.rangeKeyword = rangeKeyword;
        this.targetKeyword = targetKeyword;
    }

    public override TaskState Tick() {
        blackBoard.SetRange(rangeKeyword);
        blackBoard.SetTarget(targetKeyword);
        if (Vector3.Distance(blackBoard.self.transform.position, blackBoard.target.transform.position) <= blackBoard.range) {
            return TaskState.Succes;
        }
        else {
            return TaskState.Failure;
        }
    }
}
