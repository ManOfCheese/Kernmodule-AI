using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The weighted random selector selects which node to trigger based on a weighted random chance (like 80/20). It returns succes if this node succeeds.
public class BTWeightedRandomSelector : ABTNode {
    public BTWeightedRandomSelector(List<ABTNode> childNodes, Blackboard blackBoard, bool isLeafNode, bool isRootNode) {
        this.childNodes = childNodes;
        this.blackBoard = blackBoard;
        this.isLeafNode = isLeafNode;
        this.isRootNode = isRootNode;
    }

    public override TaskState Tick() {
        float randomResult = Random.Range(0.0f, 1.0f);
        for (int i = 0; i < childNodes.Count; i++) {
            if (i == 0) {
                if (randomResult < blackBoard.chanceWeights[i]) {
                    return childNodes[i].Tick();
                }
            }
            else {
                if (randomResult > blackBoard.chanceWeights[i - 1] && randomResult < blackBoard.chanceWeights[i]) {
                    return childNodes[i].Tick();
                }
            }
        }
        return TaskState.Failure;
    }
}
