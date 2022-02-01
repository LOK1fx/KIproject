using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    void AddItem(InventoryItem item);
    bool TryGrabItem<T>(out InventoryItem item);
    InventoryItem[] GetAllItems();
}