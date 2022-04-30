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
        return GetItemStacks(itemID)[0];
    }

    public List<ItemStack> GetItemStacksForQuantity(string itemID, int quantity)
    {
        List<ItemStack> list = new List<ItemStack>();

        int currentQuantity = 0;

        foreach (ItemStack item in InventoryItems.Values.Where(i => i.Item.ID == itemID))
        {
            if (currentQuantity == quantity)
                break;

            if (item.Quantity < quantity - currentQuantity)
            {
                currentQuantity += item.Quantity;
            }
            else
            {
                currentQuantity += quantity - currentQuantity;
            }

            list.Add(item);
        }

        return list;
    }

    public List<ItemStack> GetItemStacks(string itemID)
    {
        return InventoryItems.Values.Where(i => i.Item.ID == itemID).ToList();
    }

    public bool HasItem(string itemID, int quantity = 1)
    {
        return HasItem(ItemDatabase.GameItems[itemID], quantity);
    }

    public bool HasItem(Item item, int quantity = 1)
    {

        return GetItemAmount(item) >= quantity;
    }

    public int GetItemAmount(Item item)
    {
        int amount = 0;

        foreach (ItemStack itemStack in InventoryItems.Values.Where(i => i.Item == item))
        {
            amount += itemStack.Quantity;
        }

        return amount;
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
        {
            InventoryItems[slot] = itemStack;
            GameManager.instance.QuestManager.QuestTracker.CheckTrackedItem(itemStack.Item);
        }

        Debug.Log($"Set slot: {slot} to {itemStack}.");
    }

    public void SetItemStackQuantity(ItemStack itemStack, int quantity)
    {
        itemStack.Quantity = quantity;

        GameManager.instance.QuestManager.QuestTracker.CheckTrackedItem(itemStack.Item);
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
    public void RemoveItem(string type, int quantity = 1)
    {
        if (quantity < 0)
        {
            RemoveItems(GetItemStacks(type));
            return;
        }

        RemoveItems(GetItemStacksForQuantity(type, quantity), quantity);
    }

    public void RemoveItem(Item item, int quantity = 1)
    {
        RemoveItem(item.ID, quantity);
    }

    public void RemoveItem(ItemStack itemStack)
    {
        if (itemStack != null)
            RemoveItem(GetSlot(itemStack));
    }

    public void RemoveItem(int slot)
    {
        if (InventoryItems.ContainsKey(slot))
        {
            GameManager.instance.QuestManager.QuestTracker.CheckTrackedItem(InventoryItems[slot].Item);
            SetSlot(slot, null);
        }
    }

    public void RemoveItems(params ItemStack[] itemStack)
    {
        foreach (ItemStack item in itemStack)
            RemoveItem(item);
    }

    public void RemoveItems(List<ItemStack> itemStack)
    {
        foreach (ItemStack item in itemStack)
            RemoveItem(item);
    }

    public void RemoveItems(List<ItemStack> itemStack, int quantity)
    {

        int currentQuantity = 0;

        List<ItemStack> listToRemove = new();

        foreach (ItemStack item in itemStack)
        {
            if (currentQuantity == quantity)
                break;

            int difference = quantity - currentQuantity;

            if (item.Quantity <= difference)
            {
                currentQuantity += item.Quantity;
                listToRemove.Add(item);
            }
            else
            {
                currentQuantity += difference;
                SetItemStackQuantity(item, quantity - difference);
            }
        }

        RemoveItems(listToRemove);

    }
    #endregion

}
