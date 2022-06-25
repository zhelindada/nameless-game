using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Inventory")]
public class InventoryInitializer : ScriptableObject
{
    public List<InventoryItem> initialItems;
}
