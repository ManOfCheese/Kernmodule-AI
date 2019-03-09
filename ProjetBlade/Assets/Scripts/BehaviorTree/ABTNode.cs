using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskState { Succes, Failure, Running };

public abstract class ABTNode : MonoBehaviour {

    public ABTNode childNode;
    public bool isRootNode;
    public bool isLeafNode;

    public virtual TaskState Tick() {
        return TaskState.Failure;
    }

}
