using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Item Data/Gun")]
public class GunData : ItemData
{

    [SerializeField] private GameObject _projectile;

    public float Cooldown;

    public override void OnUse(Vector2 loc, Vector2 dir)
    {
        GameObject oj = Instantiate(_projectile, parent: GameObject.Find("Projectiles").transform);
        oj.transform.position = loc + dir;
        DamageCollider proj = oj.GetComponent<DamageCollider>();
        proj.SetTravelDirection(dir);
    }
}
