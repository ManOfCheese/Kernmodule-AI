using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveState : State {

    private GoblinDefensiveBehaviorTree behaviorTree;

    private void Start() {
        behaviorTree = GetComponent<GoblinDefensiveBehaviorTree>();
    }

    public override void EnterState() {
        behaviorTree.treeActive = true;
        behaviorTree.StartBehaviorTree();
    }

    public override void ExitState() {
        behaviorTree.treeActive = false;
        behaviorTree.StopBehaviorTree();
    }

    public override void UpdateState() {

    }
}
