using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum InventorySlotType
{
    EQUIPMENT, ITEM
}

public class UIInventory
{

    public Inventory Inventory;

    public Dictionary<int, InventorySlotComponent> InventorySlots;

    public UIInventory(Inventory inventory)
    {
        Inventory = inventory;

        InventorySlots = new Dictionary<int, InventorySlotComponent>();
    }

    public void InitializeInventory()
    {
        for (int i = 0; i < Inventory.Size; i++)
        {
            var slot = GameManager.instance.UIInventoryManager.CreateSlot(Inventory);
            slot.name = $"InventorySlot[{i}]";
            var invSlot = slot.GetComponent<InventorySlotComponent>();
            invSlot.Initialize(i, this);
            InventorySlots.Add(i, invSlot);
        }

        RefreshInventory();
    }

    public void RefreshInventory()
    {
        if (Inventory.InventoryItems.Count > 0)
        {
            foreach (KeyValuePair<int, ItemStack> kvp in Inventory.InventoryItems)
            {
                GameManager.instance.UIInventoryManager.SetSlot(this, kvp.Key, kvp.Value, false);
            }
        }

        if (Inventory.InventoryType == InventoryType.INVENTORY_PLAYER)
        {
            foreach (var kvp in Inventory.Equipment)
            {
                GameManager.instance.UIInventoryManager.SetSlot(UIInventoryManager.EquipmentSlots[kvp.Key], new ItemStack(kvp.Value, 1), false);
            }
        }
    }

    public void UnloadInventory(bool remove = false)
    {

        if (remove)
        {
            foreach(var slot in InventorySlots.Values)
            {
                GameManager.instance.UIInventoryManager.DestroySlot(slot);
            }

            InventorySlots.Clear();

            return;
        }

        foreach (var slot in InventorySlots.Values)
        {
            var item = slot.InventoryItem;

            if (item == null)
                continue;

            GameManager.instance.UIInventoryManager.ClearSlot(slot);
        }
    }

}
