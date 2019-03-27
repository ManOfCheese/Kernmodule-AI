using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskState { Succes, Failure, Running };

public abstract class ABTNode {
    protected List<ABTNode> childNodes;
    protected Blackboard blackBoard;
    protected bool isRootNode;
    protected bool isLeafNode;
    public TaskState taskState;

    public virtual TaskState Tick() {
        return TaskState.Failure;
    }
}
