using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Item Data/Shovel")]
public class ShovelData : ItemData
{

    [SerializeField] private GameObject _hole;
    [SerializeField] private float _distance = 3f;

    public float Cooldown;

    public override void OnUse(Vector2 loc, Vector2 dir)
    {
        GameObject oj = Instantiate(_hole, parent: GameObject.Find("Holes").transform);
        oj.transform.position = loc + dir * _distance;
    }
}