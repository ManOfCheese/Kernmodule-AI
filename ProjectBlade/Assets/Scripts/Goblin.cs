using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour {
    public Blackboard goblinBlackboard;

    //States.
    private StateMachine stateMachine;
    private IdleState idleState;
    private AgressiveState agressiveState;
    private DefensiveState defensiveState;
    private SwarmState swarmState;
    private SuicideState suicidalState;

    private void Awake() {
        stateMachine = GetComponent<StateMachine>();
        idleState = GetComponent<IdleState>();
        agressiveState = GetComponent<AgressiveState>();
        Debug.Log(agressiveState);
        defensiveState = GetComponent<DefensiveState>();
        swarmState = GetComponent<SwarmState>();
        swarmState.AStarUnit = goblinBlackboard.unit;
        swarmState.boidUnit = GetComponent<BoidAgent>();
        suicidalState = GetComponent<SuicideState>();
    }

    public void EnterAgressiveState() {
        stateMachine.ChangeState(agressiveState);
    }

    public void EnterDefensiveState() {
        stateMachine.ChangeState(defensiveState);
    }

    public void EnterSwarmState() {
        stateMachine.ChangeState(swarmState);
    }

    public void RecieveDamage(int amount) {
        goblinBlackboard.health -= amount;
        if (goblinBlackboard.health <= 0) {
            Destroy(this.gameObject);
        }
    }

    public void ThrowRock(GameObject projectile, Transform target) {
        GameObject newRock = Instantiate(projectile, transform.position + (transform.up * 2), Quaternion.identity);
        newRock.GetComponent<Rigidbody>().velocity = BallisticVel(target);
    }

    Vector3 BallisticVel(Transform target) {
        Vector3 dir = target.position - transform.position; // get target direction
        float h = dir.y;  // get height difference
        dir.y = 0;  // retain only the horizontal direction
        float dist = dir.magnitude;  // get horizontal distance
        dir.y = dist;  // set elevation to 45 degrees
        dist += h;  // correct for different heights
        float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude);
        return vel * dir.normalized;  // returns Vector3 velocity
    }

    private void Update() {
        if (goblinBlackboard.health < goblinBlackboard.suicidalThreshold) {
            stateMachine.ChangeState(suicidalState);
        }
    }
}
