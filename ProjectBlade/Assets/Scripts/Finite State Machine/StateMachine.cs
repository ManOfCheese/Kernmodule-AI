using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {
    public State CurrentState { get; private set; }

    private void Awake() {
        CurrentState = null;
    }

    private void Update() {
        if (CurrentState != null) {
            CurrentState.UpdateState();  
        }
    }

    //Switch to a new state.
    public void ChangeState(State newState) {
        Debug.Log("Entering " + newState);
        if (CurrentState != null) {
            CurrentState.ExitState();
        }
        CurrentState = newState;
        CurrentState.EnterState();
    }
}

//Abstract class containing all the functionality a state should have.
public abstract class State : MonoBehaviour {
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
}

