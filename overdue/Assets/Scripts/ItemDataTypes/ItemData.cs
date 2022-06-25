using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Gun,
    Shovel,
    General,
    Empty
}

[CreateAssetMenu(menuName = "Item Data/General")]
public class ItemData : ScriptableObject
{
    public ItemType Type;
    public string Name;
    public Sprite InventoryIcon;
    public Sprite WorldSprite;
    [TextArea(10,100)]
    public string Description;

    private void Awake()
    {
    }

    public virtual void OnUse(Vector2 loc, Vector2 dir) { 
        
    }

    public virtual void OnPickUp()
    {

    }
}
