using System.Collections.Generic;

public class PlayerInventory : IInventory
{
    public List<InventoryItem> Guns { get; private set; }
    public List<InventoryItem> Items { get; private set; }

    public PlayerInventory()
    {
        Guns = new List<InventoryItem>();
        Items = new List<InventoryItem>();
    }

    public void AddItem(InventoryItem item)
    {
        Items.Add(item);
    }

    public InventoryItem[] GetAllItems()
    {
        return Items.ToArray();
    }

    public bool TryGrabItem<T>(out InventoryItem item)
    {
        foreach (var i in Items)
        {
            if(i is T)
            {
                item = i;

                return true;
            }
        }

        item = null;

        return false;
    }
}
