using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine {
    public class StateMachine<T> {

        public State<T> CurrentState { get; private set; }
        public T Owner;

        //Constructor.
        public StateMachine(T _owner) {
            Owner = _owner;
            CurrentState = null;
        }

        //Change our current state.
        public void ChangeState(State<T> _newState) {
            //If we are in a state.
            if (CurrentState != null) {         
                //Run the ExitState of our current state.
                CurrentState.ExitState(Owner);  
            }
            //Set the current state to be our new state.
            CurrentState = _newState;           
            //Run the EnterState of our new state.
            CurrentState.EnterState(Owner);     
        }


        //Update this state.
        public void Update() {
            //If we are in a state.
            if (CurrentState != null) {
                //Update that state.
                CurrentState.UpdateState(Owner);  
            }
        }
    }


    //Abstract class containing all the functionality a state should have.
    public abstract class State<T> {
        public abstract void EnterState(T _owner);
        public abstract void ExitState(T _owner);
        public abstract void UpdateState(T _owner);
        public abstract void UpdateTarget(T _owner);
    }
}

