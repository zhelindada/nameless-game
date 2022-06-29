using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles decision making for an entity based on its internal finite state machine
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class AI<T> where T : Entity
{
    public T Owner;
    public FiniteStateMachine<T> FiniteStateMachine;
    public float StateUpdateCooldown;

    public virtual void Initialise(T owner, State<T> startingState) {
        Owner = owner;
        FiniteStateMachine = new FiniteStateMachine<T>(owner, startingState);
    }

    public virtual void Update() {
        FiniteStateMachine.UpdateStateMachine();
    }
}
