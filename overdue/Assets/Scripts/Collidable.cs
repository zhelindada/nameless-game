using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    private Collider2D collider2d;
    private Collider2D[] hits = new Collider2D[10];
    public ContactFilter2D filter;


    private void Awake()
    {
        if (collider2d == null) {
            collider2d = GetComponent<Collider2D>();
        }
    }
    private void Update()
    {
        ProcessCollision();
    }

    public void OnHit()
    {
        Debug.Log("Collidable: OnHit()");
    }

    private void ProcessCollision()
    {
        collider2d.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            Collider2D c = hits[i];
            if (c == null)
                continue;
            if (c.TryGetComponent(out Projectile hitter))
            {
                OnHit();
            }
            hits[i] = null;
            break;
        }
    }
}
