using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversable : Interactable
{
    [SerializeField] protected TextAsset inkJson;
    private new void Update()
    {
        base.Update();
        if (playerInRange)
            _Interactable = true;
        else
            _Interactable = false;
    }
    public override void OnInteracted()
    {

        Debug.Log("Conversable1: " + gameObject.name);
        base.OnInteracted();
        Debug.Log("Conversable2: " + gameObject.name);
        DialogueManager.Instance.StartStory(inkJson);
    }
}
