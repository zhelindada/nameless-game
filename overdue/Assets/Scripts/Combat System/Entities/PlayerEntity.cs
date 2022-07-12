using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{

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

    }

    public override void TakeDamage(float dmg)
    {
        
    }

    public override void GetHitBy(DamageCollider dmg)
    {
        throw new System.NotImplementedException();
    }
}
