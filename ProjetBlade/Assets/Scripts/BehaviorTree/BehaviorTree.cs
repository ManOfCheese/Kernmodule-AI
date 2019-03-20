using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree : MonoBehaviour {
    public List<ABTNode> behaviorTreeNodes;
    public List<int> nodeChildrenAmount;

    private void Start() {
        InstantiateTree();
    }

    public void InstantiateTree() {
        //If there is a behaviortree.
        if (behaviorTreeNodes.Count > 0) {
            //Go through it and for each node assign the next x nodes in the list as it's children where x is the nodeChildrenAmount corresponding to the node.
            for (int i = 0; i < behaviorTreeNodes.Count; i++) {
                //If this is the first node in the list it is the root node.
                if (i == 0) {
                    behaviorTreeNodes[i].isRootNode = true;
                }
                //If the node has no children it is a leaf node.
                if (nodeChildrenAmount[i] == 0) {
                    behaviorTreeNodes[i].isLeafNode = true;
                }
                for (int j = 0; j < nodeChildrenAmount[i]; j++) {
                    bool firstChildAdded = false;
                    //This must only happen on the first iteration otherwise the next node in the list will be added multiple times.
                    if (!firstChildAdded) {
                        //Add the node next node as it's child node.
                        behaviorTreeNodes[i].childNodes.Add(behaviorTreeNodes[i + 1]);
                        firstChildAdded = true;
                    }
                    //For every node after the current one check if it has 0 children.
                    for (int k = i; k < behaviorTreeNodes.Count; k++) {
                        //Bool for checking if we have reached the end of this first path.
                        bool leafReached = false;
                        //If we have reached the leafNodes of this first path set leafReached to true.
                        if (nodeChildrenAmount[k] == 0) {
                            leafReached = true;
                        }
                        //If we have reached the leafNode the next node with more than 0 children is our next childnode.
                        if (leafReached == true && nodeChildrenAmount[k] > 0) {
                            behaviorTreeNodes[i].childNodes.Add(behaviorTreeNodes[k]);
                            break;
                        }
                    }
                }
            }

        }
    }

    //go through all nodes.
    //for the children amount of current node set the next x nodes as childnodes skipping all childchildnodes until you reach a node that has no parent.
}