using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    // shows the 0, 1, 2 and 3 slots only
    // have to resolve this through limiting the size to only that

    [SerializeField] private int _maxShowSlotNumber;
    [SerializeField] private GameObject _inventorySlot;

    private Inventory inv;

    public List<Transform> children;

    public void RefreshUI() {
        foreach (Transform t in children) {
            Destroy(t.gameObject);
        }
        children = new List<Transform>();

        int size = Mathf.Min(_maxShowSlotNumber, inv.GetSize());
        for (int i = 0; i < size; i++)
        {
            GameObject newSlot = Instantiate(_inventorySlot, transform);
            newSlot.name = "Slot " + i;

            newSlot.TryGetComponent(out InventorySlotUI ui);

            InventoryItem item = inv.PeekAtSlot(i);

            if (item.GetItemType() != ItemType.Empty)
            {
                string setName = item.GetName();
                if (i == inv.currentlySelectedItemSlot)
                {
                    setName = "+" + setName;
                }

                ui.SetName(setName);
                ui.SetStackSize(item.StackSize);
            }
            else {

                ui.SetAsEmpty();
            }

            newSlot.transform.SetAsFirstSibling();
            children.Add(newSlot.transform);

        } 
    }

    public void SetInventory(Inventory inventory) {
        inv = inventory;
        RefreshUI();
    }


}
