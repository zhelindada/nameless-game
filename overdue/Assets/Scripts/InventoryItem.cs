using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{

    private string _name;

    public Item Item;
    public int StackSize = 1;

    public InventoryItem(Item item, int stackSize = 1) {
        Item = item;
        _name = item.data.name;
        StackSize = stackSize;
    }


    public void AddToStack() {
        StackSize++;
    }

    public void RemoveFromStack() {
        StackSize--;
    }

    public void UseItem(Vector2 pos, Vector2 dir)
    {
        Item.OnUse(pos, dir);
    }

    public string GetName() {
        if (GetItemType() != ItemType.Empty)
            return Item.data.name;
        return "";
    }

    public ItemType GetItemType() {
        return Item.data.Type;
    }
}
