using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private int size;
    private Dictionary<int, InventoryItem> itemMap;
    private int nextEmptySlot;


    public int currentlySelectedItemSlot;
    public InventoryItem CurrentItem {
        get { return PeekAtSlot(currentlySelectedItemSlot); }
    }

    public Inventory(int size) {
        Initialize(size);
    }
    public Inventory(int size, InventoryInitializer invIniter)
    {
        Initialize(size);
        for (int i = 0; i < invIniter.initialItems.Count; i++)
        {
            itemMap[i] = invIniter.initialItems[i];
        }
        foreach (var item in itemMap)
        {
            /*Debug.Log(item.Key + " " + item.Value.GetItemType());*/
        }
    }
    private void Initialize(int size) {
        this.size = size;
        itemMap = new Dictionary<int, InventoryItem>();
        // populate with "empty"
        for (int i = 0; i < size; i++)
        {
            itemMap.Add(i, new InventoryItem(new Item(ItemType.Empty)));
        }
        currentlySelectedItemSlot = 0;
    }
    private int NextEmptySlot()
    {
        for (int i = 0; i < size; i++)
        {
            if (itemMap[i].GetItemType() == ItemType.Empty)
                return i;
        }
        return -1;
    }
    public bool AddItem(ItemData itemData)
    {
        nextEmptySlot = NextEmptySlot();
        // if the inventory already contains an entry with this item
        int slotWithItem = FindItemSlot(itemData);
        if (slotWithItem > -1)
        {
            itemMap[slotWithItem].AddToStack();
            Debug.Log("Inventory: Added onto stack " + itemData + " at " + slotWithItem);
            return true;
        }

        // find a new empty slot
        nextEmptySlot = NextEmptySlot();
        if(nextEmptySlot == -1)
        {
            Debug.Log("Inventory: Inventory is full!");
            return false;
        }
        itemMap.Remove(nextEmptySlot);
        itemMap.Add(nextEmptySlot, new InventoryItem(new Item(ItemType.Empty)));
        Debug.Log("Inventory: Added to new stack " + itemData.name + " at " + nextEmptySlot);

        return true;
    }

    public void RemoveItem(ItemData itemData) {
        foreach (var item in itemMap.Keys)
        {
            if(itemMap[item].Item.data.Equals(itemData)) {
                itemMap[item].RemoveFromStack();
                if (itemMap[item].StackSize == 0)
                {
                    itemMap[item] = new InventoryItem(new Item(ItemType.Empty));
                    Debug.Log("Removed all item of " + itemData.name);
                    return;
                }
            }
        }
        nextEmptySlot = NextEmptySlot();
    }

    public bool IsFull() {
        foreach (var item in itemMap.Values) {
            if (item.GetItemType() == ItemType.Empty)
                return false;
        }
        return true;
    }



    public int FindItemSlot(ItemData itemData) {
        for (int i = 0; i < size; i++)
        {
            if (itemMap[i].Item.data == itemData) {
                return i;
            }
        }
        return -1;
    }

    public InventoryItem PeekAtSlot(int i) {
        return itemMap[i];
    }

    public int GetSize() {
        return size;
    }

    public new string ToString() {
        string output = "";
        foreach (var item in itemMap)
        {
            output += "Slot " + item.Key + " contains item " + item.Value.GetName() + " of stack " + item.Value.StackSize + ". \n";
        }

        return output;
    }
}
