using System.Collections.Generic;
using UnityEngine;


/*
 * Manager used to specifically handle UI components only of the inventory system.
 */
public class UIInventoryManager : MonoBehaviour
{

    public GameObject SlotPrefab;
    public GameObject ItemPrefab;
    public GameObject InventorySlots;
    public GameObject SortingSlot;
    public GameObject ContainerSlots;
    public int ContainerOpenOffset = 250;

    public UIPanelComponent PlayerInventoryPanel;
    public GameObject ContainerInventoryPanel;

    public UIInventory PlayerUIInventory;
    public UIInventory ContainerUIInventory;

    public InventoryItemComponent DraggedItem;
    public InventorySlotComponent DraggedOriginSlot;

    public UIReplicaSpriteComponent UIReplicaSprite;

    public static Dictionary<EquipmentSlot, InventorySlotComponent> EquipmentSlots;

    public Player Player;
    

    void Awake()
    {
        EquipmentSlots = new Dictionary<EquipmentSlot, InventorySlotComponent>();
        SetPlayerInventory(new UIInventory(Player.Inventory));
    }

    public void OpenContainer(Inventory inventory)
    {
        if (ContainerInventoryPanel.activeInHierarchy && ContainerUIInventory != null)
            return;

        ContainerUIInventory = new UIInventory(inventory);

        ContainerUIInventory.InitializeInventory();

        if (!PlayerInventoryPanel.IsOpen())
            PlayerInventoryPanel.Show();
        ContainerInventoryPanel.SetActive(true);

        PlayerInventoryPanel.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -ContainerOpenOffset);
    }

    public void CloseContainer()
    {
        if (!ContainerInventoryPanel.activeInHierarchy || ContainerUIInventory == null)
            return;

        ContainerUIInventory.UnloadInventory(true);

        ContainerInventoryPanel.SetActive(false);

        PlayerInventoryPanel.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        PlayerInventoryPanel.Hide();
    }

    public void SetPlayerInventory(UIInventory uIInventory)
    {
        if (PlayerUIInventory != null)
            PlayerUIInventory.UnloadInventory();

        PlayerUIInventory = uIInventory;

        foreach (var slot in EquipmentSlots)
        {
            slot.Value.UIInventory = PlayerUIInventory;
        }

        uIInventory.Inventory.InventoryType = InventoryType.INVENTORY_PLAYER;
        uIInventory.InitializeInventory();
    }

    public void ReturnDraggedItem()
    {
        if (DraggedItem == null || DraggedOriginSlot == null)
            return;

        if (EquipmentSlots.ContainsValue(DraggedOriginSlot))
        {
            EquipItemOnSlot((EquipmentSlotComponent) DraggedOriginSlot);
        } else
        {
            DropItemOntoSlot(DraggedOriginSlot);
        }
        
        ClearDraggedItem(false);
    }

    public void RefreshCharacterDisplay()
    {
        UIReplicaSprite.LoadSpriteData();
    }

    public void DropItemOntoSlot(InventorySlotComponent newSlot, InventoryItemComponent droppedItem = null)
    {
        if (droppedItem == null && DraggedItem != null)
            droppedItem = DraggedItem;

        // Unequipping armour.
        if (IsEquipmentSlot(DraggedOriginSlot))
        {
            if (newSlot.InventoryItem != null)
            {
                if (droppedItem.ItemStack.Item.EquipmentSlot != newSlot.InventoryItem.ItemStack.Item.EquipmentSlot)
                {
                    ReturnDraggedItem();
                    return;
                }

                SwapSlots(newSlot, DraggedOriginSlot, droppedItem);
                Player.SetEquippedSlot(droppedItem.ItemStack.Item.EquipmentSlot, droppedItem.ItemStack.Item);
                RefreshCharacterDisplay();
                return;
            }

            Player.SetEquippedSlot(droppedItem.ItemStack.Item.EquipmentSlot, null);
            RefreshCharacterDisplay();
            SetSlot(newSlot, droppedItem);
            return;
        }

        // Dropping stack onto another stack.
        if (newSlot.InventoryItem != null)
        {
            // If item dropped onto slot containing different type, swap.
            if (newSlot.InventoryItem.ItemStack.Item.ID != droppedItem.ItemStack.Item.ID)
            {
                if (DraggedOriginSlot.InventoryItem != null)
                {
                    DropItemOntoSlot(DraggedOriginSlot, droppedItem);
                    return;
                }

                SwapSlots(newSlot, DraggedOriginSlot, droppedItem);
                return;
            }

            MergeStacks(newSlot, DraggedItem);
            Destroy(DraggedItem.gameObject);
            return;
        }

        DraggedOriginSlot.UIInventory.Inventory.SetSlot(DraggedOriginSlot.Slot, null);
        SetSlot(newSlot, droppedItem, true);
    }

    public void EquipItemOnSlot(EquipmentSlotComponent newSlot, InventoryItemComponent droppedItem = null)
    {
        if (droppedItem == null && DraggedItem != null)
            droppedItem = DraggedItem;

        if (newSlot.InventoryItem != null)
        {
            Debug.Log("Detected swapping equipment item");
            if (droppedItem.ItemStack.Item.EquipmentSlot != newSlot.EquipmentSlot)
            {
                ReturnDraggedItem();
                return;
            }

            SwapSlots(newSlot, DraggedOriginSlot, droppedItem);

            Player.SetEquippedSlot(newSlot.EquipmentSlot, droppedItem.ItemStack.Item);
            RefreshCharacterDisplay();
            return;
        }

        SetSlot(newSlot, droppedItem, false);

        if (DraggedOriginSlot != newSlot)
        {
            Player.SetEquippedSlot(newSlot.EquipmentSlot, droppedItem.ItemStack.Item);
            RefreshCharacterDisplay();
            DraggedOriginSlot.UIInventory.Inventory.SetSlot(DraggedOriginSlot.Slot, null);
        }
        
    }


    public void ClearDraggedItem(bool destroy = false)
    {
        if (DraggedItem == null)
            return;

        Debug.Log($"Clearing dragged item. Destroy: {destroy}");

        if(destroy)
            Destroy(DraggedItem.gameObject);

        DraggedItem = null;
        DraggedOriginSlot = null;
    }

    public void SwapSlots(InventorySlotComponent newSlot, InventorySlotComponent originSlot, InventoryItemComponent originItem)
    {
        Debug.Log($"Swapped slots {newSlot} {originSlot}");
        SetSlot(originSlot, newSlot.InventoryItem, true);
        SetSlot(newSlot, originItem, true);
    }

    public void MergeStacks(InventorySlotComponent destinationSlot, InventoryItemComponent originItem)
    {
        destinationSlot.InventoryItem.ItemStack += originItem.ItemStack;
        destinationSlot.InventoryItem.UpdateQuantity();
        destinationSlot.UIInventory.Inventory.SetSlot(destinationSlot.Slot, destinationSlot.InventoryItem.ItemStack);

        if (destinationSlot != DraggedOriginSlot)
            DraggedOriginSlot.UIInventory.Inventory.SetSlot(DraggedOriginSlot.Slot, null);
    }

    public void SetSlot(UIInventory inventory, int slot, ItemStack itemStack, bool modifyInventory = true)
    {
        InventorySlotComponent invSlot = inventory.InventorySlots[slot];

        SetSlot(invSlot, CreateInventoryItemComponent(invSlot, itemStack), modifyInventory);

    }

    public void SetSlot(InventorySlotComponent slot, ItemStack itemStack, bool modifyInventory = true)
    {
        SetSlot(slot, CreateInventoryItemComponent(slot, itemStack), modifyInventory);

    }

    public void SetSlot(InventorySlotComponent slot, InventoryItemComponent invItem, bool modifyInventory = true)
    {
        Debug.Log($"UI SLOT SET: {slot} - {invItem.ItemStack} - {modifyInventory}");

        if (modifyInventory)
        {
            Debug.Log(slot.Slot);
            Debug.Log(slot.UIInventory);
            Debug.Log(slot.UIInventory.Inventory);
            Debug.Log(invItem);
            Debug.Log(invItem.ItemStack);
            slot.UIInventory.Inventory.SetSlot(slot.Slot, invItem.ItemStack);
        }
            

        var itemRect = invItem.gameObject.GetComponent<RectTransform>();
        var slotRect = slot.gameObject.GetComponent<RectTransform>();
        itemRect.SetParent(slotRect, false);
        itemRect.localPosition = new Vector2(0, 0);
        slot.InventoryItem = invItem;
    }

    public InventoryItemComponent CreateInventoryItemComponent (Transform location, ItemStack itemStack)
    {
        var invItem = Instantiate(ItemPrefab, location).GetComponent<InventoryItemComponent>();

        invItem.ItemStack = itemStack;
        invItem.SetTexture(itemStack.Item.InventoryIcon);
        invItem.UpdateQuantity();
        invItem.gameObject.name = itemStack.Item.ID;

        return invItem;
    }

    public InventoryItemComponent CreateInventoryItemComponent(InventorySlotComponent slot, ItemStack itemStack)
    {
        return CreateInventoryItemComponent(slot.gameObject.transform, itemStack);
    }

    public GameObject CreateSlot(Inventory inventory)
    {
        return Instantiate(SlotPrefab, inventory.InventoryType == InventoryType.INVENTORY_PLAYER ? InventorySlots.transform : ContainerSlots.transform);
    }

    public void ClearSlot(InventorySlotComponent slot)
    {
        if (slot.InventoryItem == null)
        {
            return;
        }

        Destroy(slot.InventoryItem.gameObject);
        slot.InventoryItem = null;
    }

    public void DestroySlot(InventorySlotComponent slot)
    {
        Destroy(slot.gameObject);
    }

    public void LoadPlayerInventoryUI()
    {
        if (PlayerUIInventory != null)
            PlayerUIInventory.InitializeInventory();
    }

    public void RefreshPlayerInventoryUI()
    {
        if (PlayerUIInventory != null)
            PlayerUIInventory.RefreshInventory();
    }

    public void UnloadPlayerInventoryUI()
    {
        if (PlayerUIInventory != null)
            PlayerUIInventory.UnloadInventory();

        foreach(var slot in EquipmentSlots)
        {
            var item = slot.Value.InventoryItem;

            if (item == null)
                continue;

            ClearSlot(slot.Value);
        }
    }

    public bool IsEquipmentSlot(InventorySlotComponent slot)
    {
        return EquipmentSlots.ContainsValue(slot);
    }

}
