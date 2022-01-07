using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum InventoryType
{
    INVENTORY_PLAYER, INVENTORY_CONTAINER, INVENTORY_ENTITY
}

public class Inventory : MonoBehaviour
{

    public InventoryType InventoryType;
    public int Size = 18;
    public Dictionary<int, ItemStack> InventoryItems;
    public Dictionary<EquipmentSlot, Item> Equipment;

    private void Awake()
    {
        InventoryItems = new Dictionary<int, ItemStack>();
        Equipment = new Dictionary<EquipmentSlot, Item>();
    }

    // Find the first empty slot within the inventory.
    private int FindFirstEmpty()
    {

        for (int i = 0; i < Size; i++)
        {

            if (InventoryItems.ContainsKey(i))
                continue;

            return i;
        }

        return -1;
    }

    private ItemStack GetItemStack(string itemID)
    {

        ItemStack itemStack = null;

        try
        {
            InventoryItems.Values.Where(i => i.Item.ID == itemID).FirstOrDefault();
        }
        catch { }

        return itemStack;
    }

    private int GetSlot(ItemStack itemStack)
    {
        int i = -1;

        try
        {
            i = InventoryItems.Where(kvp => kvp.Value == itemStack).FirstOrDefault().Key;
        }
        catch { }


        return i;
    }

    public void SetSlot(int slot, ItemStack itemStack)
    {
        if (itemStack == null)
        {
            InventoryItems.Remove(slot);
        }
        else
            InventoryItems[slot] = itemStack;

        Debug.Log($"Set slot: {slot} to {itemStack}.");
    }

    #region Adding Items
    public void AddItem(Item item)
    {
        AddItem(item, FindFirstEmpty());
    }

    public void AddItem(Item item, int slot)
    {

        AddItem(new ItemStack(item, 1), slot);
    }

    public void AddItem(ItemStack itemStack, int slot)
    {
        InventoryItems[slot] = itemStack;
    }

    public void AddItem(ItemStack itemStack)
    {
        InventoryItems[FindFirstEmpty()] = itemStack;
    }
    #endregion

    #region Removing Items
    public void RemoveItem(string type)
    {
        RemoveItem(GetItemStack(type));
    }

    public void RemoveItem(ItemStack itemStack)
    {
        if (itemStack != null)
            RemoveItem(GetSlot(itemStack));
    }

    public void RemoveItem(int slot)
    {
        if (InventoryItems.ContainsKey(slot))
            InventoryItems.Remove(slot);
    }
    #endregion

}
