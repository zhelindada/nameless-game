using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T>
{
    public float time;

    public virtual void EnterState(T _owner)
    {
        time = Time.time;
    }

    public virtual void ExitState(T _owner) {
    }

    public virtual void UpdateState(T _owner) { 
    
    }
}
