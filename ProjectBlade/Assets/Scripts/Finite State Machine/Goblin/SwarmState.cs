using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmState : State {

    public AStarUnit AStarUnit;
    public BoidAgent boidUnit;

    public override void EnterState() {
        AStarUnit.enabled = false;
        boidUnit.enabled = true;
    }

    public override void ExitState() {
        AStarUnit.enabled = true;
        boidUnit.enabled = false;
    }

    public override void UpdateState() {

    }
}
