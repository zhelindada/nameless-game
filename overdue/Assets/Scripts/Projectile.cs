using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private static int counter;
    private Collider2D collider2d;
    private Collider2D[] hits = new Collider2D[10];
    private ContactFilter2D filter;

    [SerializeField] private Sprite projectileSprite;
    [SerializeField] private Vector2 travelDirection;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float travelDistance;

    public void OnHit()
    {
        Debug.Log("Projectile: OnHit() " + gameObject.name);
        Destroy(gameObject, 0.05f);
    }

    public void SetTravelDirection(Vector2 direction) {
        travelDirection = direction;
        if (direction == Vector2.right)
            transform.Rotate(0, 0, 0);
        if (direction == Vector2.up)
            transform.Rotate(0, 0, 90);
        if (direction == Vector2.left)
            transform.Rotate(0, 0, 180);
        if (direction == Vector2.down)
            transform.Rotate(0, 0, 270);
    }

    private void Awake()
    {
        Debug.Log("Projectile: New Projectile");
        GameObject projectileParent = GameObject.FindGameObjectWithTag("projectile base");
        transform.SetParent(projectileParent.transform);
        if (TryGetComponent(out Collider2D collider))
        {
            collider2d = collider;
        }
        else {
            Debug.LogError("Projectile: Projectile does not have a Collider2D");
        }
    }

    private void Update()
    {

        Vector2 newPosition = new Vector2(transform.position.x, transform.position.y) +
            travelDirection * projectileSpeed * Time.deltaTime;

        float deltaDistance = (travelDirection * projectileSpeed * Time.deltaTime).magnitude;
        travelDistance -= deltaDistance;
        if (travelDistance <= 0) {
            Destroy(gameObject);
        }

        transform.position = newPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Projectile: OnCollisionEnter2D " + collision.collider.name);
        if (collision.collider.gameObject.TryGetComponent(out Collidable collidable)) {
            Debug.Log("Proj");
            OnHit();
        }
    }

}
