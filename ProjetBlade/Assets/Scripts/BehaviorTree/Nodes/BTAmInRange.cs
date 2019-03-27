using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The AmInRange node returns true when the AI is in range of target, otherwise it returns failure.
public class BTAmInRange : ABTNode {
<<<<<<< HEAD:ProjetBlade/Assets/Scripts/BehaviorTree/Nodes/BTAmInRange.cs
    private string rangeKeyword;
    private string targetKeyword;

    public BTAmInRange(List<ABTNode> childNodes, Blackboard blackBoard, bool isLeafNode, bool isRootNode, string targetKeyword, string rangeKeyword) {
=======
    public BTAmInRange(List<ABTNode> childNodes, BlackBoard blackBoard, bool isLeafNode, bool isRootNode) {
>>>>>>> parent of 6a1702b... Bugfixes and making player and units unwalkable on the grid.:ProjetBlade/Assets/Scripts/BehaviorTree/BTAmInRange.cs
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
