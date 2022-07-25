using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{

    public Action OnTakeDamage;
    public Action OnDeath;

    private void Awake()
    {
        health = maxHealth;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent<DamageCollider>(out DamageCollider dmg)) {
            if(dmg.DamagesPlayer)
                TakeDamage(dmg.GetDamageAmount());
        }
    }

    public override void Attack(Vector2 target) {}
    public override void Attack(Entity target) {}
    public override void MoveTowards(Vector2 target) {}
    
    public override void Die()
    {
        OnDeath?.Invoke();
        Debug.Log("Game Over - The Player Dies");
        GameManager.Instance.PlyrResCorStarter();


        gameObject.SetActive(false);
    }

    public override void TakeDamage(float dmg)
    {
        Debug.Log("PlayerEntity - player taking damage");
        health -= dmg;
        OnTakeDamage?.Invoke();
        if (health <= 0)
            Die();
    }

    public override void GetHitBy(DamageCollider dmg)
    {
        if (!dmg.DamagesPlayer)
            return;
        _invincibilityTimer = invincibleTime;
        TakeDamage(dmg.GetDamageAmount());
    }

    public void HealBy(float f) {
        health += f;
        if (health > maxHealth) {
            health = maxHealth;
        }
    }


}
