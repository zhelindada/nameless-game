using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    
    [SerializeField] private int _size = 4;
    [SerializeField] private InventoryUI _ui;
    [SerializeField] private InventoryInitializer _inventoryInitializer;

    public Inventory inventory;

    private void Initialize() {
        Init();
        if (_inventoryInitializer == null)
            inventory = new Inventory(_size);
        else
            inventory = new Inventory(_size, _inventoryInitializer);

        Debug.Log(inventory.ToString());
        if (_ui == null)
            Debug.LogError("InventoryManager: UI not set");
        RefreshUI();
    }

    private void Awake()
    {
        Initialize();
    }

    private void RefreshUI() {
        _ui.SetInventory(inventory);
        _ui.RefreshUI();
    }

    public void SelectItem(int i)
    {
        if (i > _size)
        {
            Debug.LogError("InventoryManager: TRYING TO SELECT INDEX GREATER THAN SIZE");
            return;
        }

        inventory.currentlySelectedItemSlot = i;
        RefreshUI();
    }

    public bool AddItem(ItemData itemData) {
        bool output = inventory.AddItem(itemData);
        RefreshUI();
        return output;
    }

    public void RemoveItem(ItemData itemData) {
        inventory.RemoveItem(itemData);
        RefreshUI();
    }

    public void UseItem(Vector2 pos, Vector2 dir) {
        inventory.CurrentItem.UseItem(pos, dir);
    }
    

}
