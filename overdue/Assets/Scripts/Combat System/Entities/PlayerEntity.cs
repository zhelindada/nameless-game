using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent<DamageMelee>(out DamageMelee dmg)) {
            TakeDamage(dmg.damage);
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
}
