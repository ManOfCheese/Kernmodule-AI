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

    private void Start() {
        stateMachine = GetComponent<StateMachine>();
        attackState = GetComponent<AttackState>();
        defendState = GetComponent<DefendState>();
        battleCryState = GetComponent<BattleCryState>();

        EnterAttackState();
        foreach (Goblin goblin in commanderBlackBoard.goblins) {
            goblin.EnterAgressiveState();
        }
    }

    public void EnterAttackState() {
        stateMachine.ChangeState(attackState);
    }

    public void EnterDefendState() {
        stateMachine.ChangeState(defendState);
    }

    public void EnterBattleCryState() {
        stateMachine.ChangeState(battleCryState);
    }

    public void RecieveDamage(int amount) {
        commanderBlackBoard.health -= amount;
        if (commanderBlackBoard.health <= commanderBlackBoard.healthDefenseThreshold) {
            EnterDefendState();
            foreach (Goblin goblin in commanderBlackBoard.goblins) {
                goblin.EnterDefensiveState();
            }
        }
        else if (commanderBlackBoard.health <= commanderBlackBoard.healthSwarmThreshold) {
            EnterBattleCryState();
            foreach (Goblin goblin in commanderBlackBoard.goblins) {
                goblin.EnterSwarmState();
            }
        }
        else if (commanderBlackBoard.health <= 0) {
            Destroy(this.gameObject);
        }
    }
}
