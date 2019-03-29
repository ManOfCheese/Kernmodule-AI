using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

    public State CurrentState { get; private set; }

    private void Awake() {
        CurrentState = null;
    }

    //Change our current state.
    public void ChangeState(State _newState) {
        Debug.Log("Entering " + _newState);
        //If we are in a state.
        if (CurrentState != null) {         
            //Run the ExitState of our current state.
            CurrentState.ExitState();
        }
        //Set the current state to be our new state.
        CurrentState = _newState;
        //Run the EnterState of our new state.
        CurrentState.EnterState();     
    }


    //Update this state.
    public void Update() {
        //If we are in a state.
        if (CurrentState != null) {
            //Update that state.
            CurrentState.UpdateState();  
        }
    }
}

//Abstract class containing all the functionality a state should have.
public abstract class State : MonoBehaviour {
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
}

