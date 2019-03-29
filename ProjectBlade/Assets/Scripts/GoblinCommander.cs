using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinCommander : MonoBehaviour {
    public Blackboard commanderBlackBoard;

    //States
    private StateMachine stateMachine;
    private AttackState attackState;
    private DefendState defendState;
    private BattleCryState battleCryState;

    private int damageWhileDefending;
    private int defendCount;

    private void Start() {
        stateMachine = GetComponent<StateMachine>();
        attackState = GetComponent<AttackState>();
        defendState = GetComponent<DefendState>();
        battleCryState = GetComponent<BattleCryState>();
        EnterAttackState();
    }

    private void Update() {
        //Temporary hardcode for max health.
        if (commanderBlackBoard.health >= 250 && stateMachine.CurrentState != attackState) {
            EnterAttackState();
        }
        if (commanderBlackBoard.health <= commanderBlackBoard.healthDefenseThreshold && stateMachine.CurrentState != defendState) {
            EnterDefendState();
            foreach (Goblin goblin in commanderBlackBoard.goblins) {
                goblin.EnterDefensiveState();
            }
        }
        else if (commanderBlackBoard.health <= commanderBlackBoard.healthSwarmThreshold && stateMachine.CurrentState != battleCryState) {
            EnterBattleCryState();
            foreach (Goblin goblin in commanderBlackBoard.goblins) {
                goblin.EnterSwarmState();
            }
        }
        else if (commanderBlackBoard.health <= 0) {
            Destroy(this.gameObject);
        }
    }

    //Enter the attacking state for the commander and all his minions.
    public void EnterAttackState() {
        stateMachine.ChangeState(attackState);
        foreach (Goblin goblin in commanderBlackBoard.goblins) {
            goblin.EnterAgressiveState();
        }
    }

    //Enter the defend state can only be done twice because of the health regen.
    public void EnterDefendState() {
        if (defendCount < 3) {
            stateMachine.ChangeState(defendState);
            foreach (Goblin goblin in commanderBlackBoard.goblins) {
                goblin.EnterDefensiveState();
                defendCount++;
            }
        }
    }

    //Enter the battle cry state triggering the boid algorithm in minions.
    public void EnterBattleCryState() {
        stateMachine.ChangeState(battleCryState);
        foreach (Goblin goblin in commanderBlackBoard.goblins) {
            goblin.EnterSwarmState();
        }
    }

    //Take damage, if defending it will store the damage and above a threshold exit the defend state.
    public void RecieveDamage(int amount) {
        commanderBlackBoard.health -= amount;
        if (stateMachine.CurrentState == defendState) {
            damageWhileDefending += amount;
        }
        //Temporary hardcode for breakthrough damage.
        if (damageWhileDefending >= 30 && stateMachine.CurrentState != attackState) {
            EnterAttackState();
        }
    }
}
