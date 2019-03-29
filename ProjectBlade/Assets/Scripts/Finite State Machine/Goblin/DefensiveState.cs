using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveState : State {
    private GoblinDefensiveBehaviorTree behaviorTree;

    private void Awake() {
        behaviorTree = GetComponent<GoblinDefensiveBehaviorTree>();
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
