using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{

    [SerializeField] private Collider2D collider2d;
    [SerializeField] private Collider2D[] hits = new Collider2D[10];
    public ContactFilter2D filter;

    public Inventory inventory;

    private void Awake()
    {
        if (collider2d == null)
            if (!TryGetComponent(out collider2d))
                Debug.LogError("PlayerCollect: no collider2d");
    }

    private void Update()
    {
        ProcessCollisions();
    }
    protected virtual void ProcessCollisions()
    {
        collider2d.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            Collider2D c = hits[i];
            if (c == null)
                continue;
            // <conditions>

            if (c.TryGetComponent(out ItemWorld item)) {
                
            }


            // </conditions>
            hits[i] = null;
            break;
        }
    }

}
