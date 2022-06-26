using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI<T> where T : Entity
{
    public FiniteStateMachine<T> finiteStateMachine;

    
}
