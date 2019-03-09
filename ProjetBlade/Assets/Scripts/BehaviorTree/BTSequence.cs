using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The sequence node will run through all its children returning failure if any of them fails, and running if any of them is still running.
//Only when all child nodes of the sequence have succeeded does it returns succes.
public class BTSequence : ABTNode {

    public List<ABTNode> childNodes;

    public override TaskState Tick() {
        int succesCount = 0;
        //Tick all child nodes.
        foreach (ABTNode node in childNodes) {
            if (node.Tick() == TaskState.Failure) {
                //If any fails immediately return failure.
                return TaskState.Failure;
            }
            else if (node.Tick() == TaskState.Running) {
                //Same for running.
                return TaskState.Running;
            }
            else if (node.Tick() == TaskState.Succes) {
                //If a node returns succes add it to the succes count.
                succesCount++;
            }
        }
        //If all nodes succeeded return succes.
        if (succesCount == childNodes.Count) {
            return TaskState.Succes;
        }
        else {
            Debug.Log("Sequence || No node was ticked");
            return TaskState.Failure;
        }
    }

}
