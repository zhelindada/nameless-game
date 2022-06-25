using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    public static T Instance{
        get;
        protected set;
    }

    private void Awake()
    {
        Init();
    }

    protected void Init(){
        if (Instance == null) Instance = (T)this;
    }
}
