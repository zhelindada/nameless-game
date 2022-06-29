using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    [SerializeField] private Collider2D _interactionCollider;
    private List<GameObject> _interactables = new List<GameObject>();



    /*
    private Collider2D playerCollider;
    private Collider2D[] hits = new Collider2D[10];
    public ContactFilter2D filter;

    private void Awake()
    {
        GameObject interactionCollider = transform.Find("Interaction Collider").gameObject;

        if (!interactionCollider.TryGetComponent(out playerCollider))
        {
            Debug.LogError("Player have no collider");
            playerCollider = gameObject.AddComponent<BoxCollider2D>();
        }
        filter = new ContactFilter2D();
    }

    public void Interact() {
        Debug.Log("PlayerInteraction: Player Interacting");
        //find closest collider in player interact range
        playerCollider.OverlapCollider(filter, hits);
        bool haveInteracted = false;
        for (int i = 0; i < hits.Length; i++)
        {
            Collider2D c = hits[i];
            if (c == null) continue;

            Interactable[] interactables;
            interactables = c.gameObject.GetComponents<Interactable>();
            if (interactables.Length > 0)
            {
                foreach (var iObj in interactables)
                {
                    if (iObj._Interactable)
                    {
                        iObj.OnInteracted();
                        haveInteracted = true;
                    }
                }
                Debug.Log("PlayerInteraction: interactables[" + interactables.Length + "]");
            }
            else
            {
                Debug.Log("PlayerInteraction: Empty interactable list");
            }
            if (c.gameObject.TryGetComponent(out Interactable iObj))
            {
                if (iObj._Interactable)
                {
                    iObj.OnInteracted();
                    haveInteracted = true;
                }
            }
            hits[i] = null;
            if (haveInteracted)
                return;
        }

        //call the OnInteracted() method on that interactable object
    }
    */

    private void Awake()
    {
        _interactionCollider = transform.Find("Interaction Collider").GetComponent<Collider2D>();
    }

    private void Update()
    {
        _interactables = new List<GameObject>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Interactable inter)) { 
            
        }
        if(inter!= null)
            _interactables.Add(inter.gameObject);
    }

    public void Interact() {
        Debug.Log("PlayerInteraction: Player Interacting");
        if (_interactables.Count <= 0) {
            Debug.Log("PlayerInteraction: Nothing to Interact with");
            return;
        }
        Interactable[] interactables = _interactables[0].transform.GetComponentsInChildren<Interactable>();
        foreach (var inter in interactables) {
            inter.OnInteracted();
        }
    }
}
