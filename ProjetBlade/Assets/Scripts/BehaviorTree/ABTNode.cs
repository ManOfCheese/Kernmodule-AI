using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskState { Succes, Failure, Running };

public abstract class ABTNode : MonoBehaviour {

    [HideInInspector]
    public List<ABTNode> childNodes;
    [HideInInspector]
    public TaskState taskState;
    [HideInInspector]
    public bool isRootNode;
    [HideInInspector]
    public bool isLeafNode;

    public virtual TaskState Tick() {
        return TaskState.Failure;
    }

}
