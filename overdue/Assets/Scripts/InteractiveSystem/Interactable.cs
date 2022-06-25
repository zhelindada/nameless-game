using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];
    private ContactFilter2D filter;

    [SerializeField] private bool _interactable = false;
    public bool _Interactable {
        get {
            return _interactable;
        }
        set {
            bool invokeChangeEvent = _interactable == value;
            _interactable = value;
            if(invokeChangeEvent)
                if (_interactable)
                    OnEnableInteraction?.Invoke();
                else
                    OnDisableInteraction?.Invoke();

        }
    }

    event Action OnEnableInteraction;
    event Action OnDisableInteraction;

    public bool playerInRange;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            playerInRange = false;
        }
    }
    protected virtual void Start()
    {
        if (!TryGetComponent(out boxCollider))
        {
            Debug.LogError("Interactable:Start() Interactable have no collider");
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
        }
    }

    protected virtual void Update()
    {
        //ProcessCollisions();
    }

    protected virtual void ProcessCollisions() {
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            Collider2D c = hits[i];
            playerInRange = false;
            if (c == null)
                continue;
            if (c.gameObject.name == "Player")
            {
                playerInRange = true;
            }
            hits[i] = null;
            break;
        }
    }

    public virtual void OnInteracted() {
        Debug.Log("OnInteracted Called on: " + GetType());  
    }
    public void RegisterEnableInteraction(Action action)
    {
        OnEnableInteraction += action;
    }
    public void UnregisterEnableInteraction(Action action)
    {
        OnEnableInteraction -= action;
    }
    public void RegisterDisableInteraction(Action action)
    {
        OnDisableInteraction += action;
    }
    public void UnregisterDisableInteraction(Action action)
    {
        OnDisableInteraction -= action;
    }
}
