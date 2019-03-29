using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmState : State {
    public AStarUnit aStarUnit;
    public BoidAgent boidUnit;

    public override void EnterState() {
        aStarUnit.enabled = false;
        boidUnit.enabled = true;
    }

    public override void ExitState() {
        aStarUnit.enabled = true;
        boidUnit.enabled = false;
    }

    public override void UpdateState() {

    }
}
