using System;
using UnityEngine;
/// <summary>
/// Includes entity methods that controls object logic
/// </summary>
public abstract class Entity : MonoBehaviour
{

    [SerializeField] private bool testing;
    [SerializeField] protected float _invincibilityTimer;
    protected float health;
    protected AI<Entity> AI;
    protected EntityHealthIndicator healthIndicator;

    [SerializeField] protected float maxHealth;
    [SerializeField] protected float invincibleTime = 0.16f;



    protected void Awake()
    {
        health = maxHealth;

        healthIndicator = GetComponentInChildren<EntityHealthIndicator>();
    }

    protected void Update()
    {
        _invincibilityTimer -= Time.deltaTime;
    }

    public abstract void TakeDamage(float dmg);
    public abstract void Die();
    public abstract void MoveTowards(Vector2 target);
    public abstract void Attack(Vector2 target);
    public abstract void Attack(Entity target);
    public virtual void GetHitBy(DamageCollider dmg) {
        if (_invincibilityTimer > 0)
            return;
        TakeDamage(dmg.GetDamageAmount());
        _invincibilityTimer = invincibleTime;
    }
    public float GetMaxHealth() {
        return maxHealth;
    }

    public float GetCurrentHealth() {
        return health;
    }

    // test methods
    public void TestTakeDamage() {
        if(testing) TakeDamage(1000);
    }

    public void TestDie() {
        if (testing) TakeDamage(maxHealth * 999);
    }
}
