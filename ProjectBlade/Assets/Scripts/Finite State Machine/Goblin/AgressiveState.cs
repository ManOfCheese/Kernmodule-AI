using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgressiveState : State {

    private GoblinAgressiveBehaviorTree behaviorTree;

    private void Awake() {
        behaviorTree = GetComponent<GoblinAgressiveBehaviorTree>();
    }

    public override void EnterState() {
        behaviorTree.treeActive = true;
    }

    public override void ExitState() {
        behaviorTree.treeActive = false;
    }

    public override void UpdateState() {

    }
}
