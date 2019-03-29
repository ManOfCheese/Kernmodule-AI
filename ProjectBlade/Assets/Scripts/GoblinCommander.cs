using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinCommander : MonoBehaviour {
    public Blackboard commanderBlackBoard;

    //States.
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

    public void EnterAttackState() {
        stateMachine.ChangeState(attackState);
        foreach (Goblin goblin in commanderBlackBoard.goblins) {
            goblin.EnterAgressiveState();
        }
    }

    public void EnterDefendState() {
        if (defendCount < 3) {
            stateMachine.ChangeState(defendState);
            foreach (Goblin goblin in commanderBlackBoard.goblins) {
                goblin.EnterDefensiveState();
                defendCount++;
            }
        }
    }

    public void EnterBattleCryState() {
        stateMachine.ChangeState(battleCryState);
        foreach (Goblin goblin in commanderBlackBoard.goblins) {
            goblin.EnterSwarmState();
        }
    }

    public void RecieveDamage(int amount) {
        commanderBlackBoard.health -= amount;
        if (stateMachine.CurrentState == defendState) {
            damageWhileDefending += amount;
        }
        if (damageWhileDefending >= 30 && stateMachine.CurrentState != attackState) {
            EnterAttackState();
        }
    }
}
