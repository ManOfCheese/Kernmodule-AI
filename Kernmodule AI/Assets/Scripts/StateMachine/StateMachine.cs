using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

    public State currentState; //Tracks what our current state is.

    //Change our current state.
    public void ChangeState(State newState) {
        if (currentState != null) {               //If we are in a state.
            currentState.ExitState();             //Run the ExitState of our current state.
        }
        currentState = newState;                  //Set the current state to be our new state.
        currentState.EnterState();                //Run the EnterState of our new state.
    }


    //Update this state.
    public void Update() {
        if (currentState != null) {      //If we are in a state.
            currentState.UpdateState();  //Update that state.
        }
    }
}

//Abstract class containing all the functionality a state should have.
public interface IState {
    void EnterState();
    void ExitState();
    void UpdateState();
}

public abstract class State : MonoBehaviour, IState {
    public virtual void EnterState() {
    }

    public virtual void ExitState() {
    }

    public virtual void UpdateState() {
    }
}
