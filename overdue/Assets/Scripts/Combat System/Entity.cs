using UnityEngine;
public abstract class Entity : MonoBehaviour
{
    public EntityData Data;

    public abstract void TakeDamage();
    public abstract void Die();
}
