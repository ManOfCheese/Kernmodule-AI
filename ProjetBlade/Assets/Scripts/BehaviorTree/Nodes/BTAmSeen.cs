using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The AmSeen node will return succes if target is looking at it and failure if not.
public class BTAmSeen : ABTNode {
<<<<<<< HEAD:ProjetBlade/Assets/Scripts/BehaviorTree/Nodes/BTAmSeen.cs
    private string targetKeyword;

    public BTAmSeen(List<ABTNode> childNodes, Blackboard blackBoard, bool isLeafNode, bool isRootNode, string targetKeyword) {
=======
    public BTAmSeen(List<ABTNode> childNodes, BlackBoard blackBoard, bool isLeafNode, bool isRootNode) {
>>>>>>> parent of 6a1702b... Bugfixes and making player and units unwalkable on the grid.:ProjetBlade/Assets/Scripts/BehaviorTree/BTAmSeen.cs
        this.childNodes = childNodes;
        this.blackBoard = blackBoard;
        this.isLeafNode = isLeafNode;
        this.isRootNode = isRootNode;
    }

    public override TaskState Tick() {
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
