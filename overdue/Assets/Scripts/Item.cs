using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{

    public ItemData data = Resources.Load<ItemData>("Item Datas/empty");

    public Item(ItemType type) {
        if (type == ItemType.Empty)
            return;
        object[] allData = Resources.LoadAll<ItemData>("Item Datas");
        foreach (ItemData data in allData) { 
            // TODO: improve this
            if (data.Type == type)
            {
                this.data = data;
                return;
            }
        }
    }

    public void OnUse(Vector2 playerPosition, Vector2 targetPosition) {
        Debug.Log("Item: used item of type " + GetType() + " at " + targetPosition);
        data.OnUse(playerPosition, targetPosition);
    }
    public void OnPickUp() {
        Debug.Log("Item: picked up item of type " + GetType());
        data.OnPickUp();
    }

}
