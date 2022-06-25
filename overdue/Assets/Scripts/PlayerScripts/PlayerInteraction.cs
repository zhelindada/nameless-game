using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private BoxCollider2D playerCollider;
    private Collider2D[] hits = new Collider2D[10];
    public ContactFilter2D filter;

    private void Awake()
    {
        if(!TryGetComponent(out playerCollider))
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
            interactables = c.gameObject.GetComponentsInChildren<Interactable>();
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
            /*if (c.gameObject.TryGetComponent(out Interactable iObj))
            {
                if (iObj._Interactable)
                {
                    iObj.OnInteracted();
                    haveInteracted = true;
                }
            }*/
            hits[i] = null;
            if (haveInteracted)
                return;
        }

        //call the OnInteracted() method on that interactable object
    }
    
}
