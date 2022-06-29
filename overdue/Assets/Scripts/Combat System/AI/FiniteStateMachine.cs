using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Container for states and handling switching between states
/// </summary>
/// <typeparam name="T"></typeparam>
public class FiniteStateMachine<T>
{
    public T Owner;
    public State<T> currentState;

    public FiniteStateMachine(T _owner, State<T> startingState){
        Owner = _owner;
        ChangeState(startingState);
    }

    public void ChangeState(State<T> state) {

        currentState?.ExitState(Owner);
        Debug.Log("Changing to " + state.ToString());
        currentState = state;
        Debug.Log(currentState.ToString());
        currentState.EnterState(Owner);
    }


    public void UpdateStateMachine() {
        /*Debug.Log("Updating State Machine");*/
        currentState.UpdateState(Owner);
    }
}
