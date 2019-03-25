using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The AmSeen node will return succes if target is looking at it and failure if not.
public class BTAmSeen : ABTNode {
    private string targetKeyword;

    public BTAmSeen(List<ABTNode> childNodes, BlackBoard blackBoard, bool isLeafNode, bool isRootNode, string targetKeyword) {
        this.childNodes = childNodes;
        this.blackBoard = blackBoard;
        this.isLeafNode = isLeafNode;
        this.isRootNode = isRootNode;
        this.targetKeyword = targetKeyword;
    }

    public override TaskState Tick() {
        blackBoard.SetTarget(targetKeyword);

        if (Vector3.Angle(blackBoard.target.transform.forward, blackBoard.self.transform.position - blackBoard.target.transform.position) < blackBoard.viewAngle) {
            //Debug.Log("AmSeen || Succes");
            return TaskState.Succes;
        }
        else {
            //Debug.Log("AmSeen || Failure");
            return TaskState.Failure;
        }
    }
}
