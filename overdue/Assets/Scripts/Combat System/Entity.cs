using System;
using UnityEngine;
/// <summary>
/// Includes entity methods that controls object logic
/// </summary>
public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    protected float health;
    protected AI<Entity> AI;
    protected EntityHealthIndicator healthIndicator;

    protected void Awake()
    {
        health = maxHealth;

        healthIndicator = GetComponentInChildren<EntityHealthIndicator>();
    }

    public abstract void TakeDamage(float dmg);
    public abstract void Die();
    public abstract void MoveTowards(Vector2 target);
    public abstract void Attack(Vector2 target);
    public abstract void Attack(Entity target);
    public virtual void GetHitBy(DamageCollider dmg) {
        TakeDamage(dmg.GetDamageAmount());
    }
    public float GetMaxHealth() {
        return maxHealth;
    }
}
