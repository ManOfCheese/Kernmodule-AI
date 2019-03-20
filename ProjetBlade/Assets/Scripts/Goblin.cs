using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour {

    public float behaviorTreeRecalculationDelay;
    public float MeleeRange;
    public float moveToCommanderRange;
    public float viewAngle;
    public int health;
    public int suicidalThreshold;

    //These functions must be predefined because they are immediately requested by the behavior trees.
    public AStarUnit unit;
    public BoidAgent boidUnit;
    public Animator animator;
    public GameObject moveTarget;
    public GameObject attackTarget;
    public GameObject rock;
    public GameObject dynamite;
    public List<float> chanceWeights;

    private StateMachine stateMachine;
    private AgressiveState agressiveState;
    private DefensiveState defensiveState;
    private SwarmState swarmState;
    private SuicideState suicidalState;

    private void Start() {
        stateMachine = GetComponent<StateMachine>();
        agressiveState = GetComponent<AgressiveState>();
        defensiveState = GetComponent<DefensiveState>();
        swarmState = GetComponent<SwarmState>();
        swarmState.AStarUnit = unit;
        swarmState.boidUnit = GetComponent<BoidAgent>();
        suicidalState = GetComponent<SuicideState>();
        unit = this.GetComponent<AStarUnit>();
        animator = transform.GetComponentInChildren<Animator>();
    }

    public void EnterAgressiveState() {
        if (stateMachine.CurrentState != suicidalState) {
            stateMachine.ChangeState(agressiveState);
            Debug.Log("Entered Agressive State");
        }
    }

    public void EnterDefensiveState() {
        if (stateMachine.CurrentState != suicidalState) {
            stateMachine.ChangeState(defensiveState);
            Debug.Log("Entered Defensive State");
        }
    }

    public void EnterSwarmState() {
        if (stateMachine.CurrentState != suicidalState) {
            stateMachine.ChangeState(swarmState);
            Debug.Log("Entered Swarm State");
        }
    }

    public void RecieveDamage(int amount) {
        health -= amount;
    }

    private void Update() {
        if (health < suicidalThreshold) {
            stateMachine.ChangeState(suicidalState);
        }
    }
}
