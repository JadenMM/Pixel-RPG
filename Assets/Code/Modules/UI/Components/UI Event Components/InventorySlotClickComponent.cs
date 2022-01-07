using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Component used to handle clicking on inventory slots within inventory UI.
 */
public class InventorySlotClickComponent : MonoBehaviour, IPointerClickHandler
{

    private InventorySlotComponent inventorySlot;


    private void Awake()
    {
        inventorySlot = gameObject.GetComponent<InventorySlotComponent>();

        if (inventorySlot == null)
            Debug.LogError("Error... inventory slot click component is not attached to an object with an inventory slot component.");
    }

    // Click inventory actions.
    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button != PointerEventData.InputButton.Right)
            return;

        bool slotOccupied = inventorySlot.InventoryItem != null;

        if (GameManager.instance.UIInventoryManager.DraggedItem == null)
            return;

        if (slotOccupied)
        {
            if (inventorySlot.InventoryItem.ItemStack.Item.ID != GameManager.instance.UIInventoryManager.DraggedItem.ItemStack.Item.ID)
                return;

            if (GameManager.instance.UIInventoryManager.IsEquipmentSlot(inventorySlot))
            {
                GameManager.instance.UIInventoryManager.ReturnDraggedItem();
                return;
            }

            // If occupied slot being dropped onto is same type as item dragged, add one to quantity.
            var stack = new ItemStack(inventorySlot.InventoryItem.ItemStack.Item, 1);

            GameManager.instance.UIInventoryManager.ClearSlot(inventorySlot);
            GameManager.instance.UIInventoryManager.SetSlot(inventorySlot, stack + 1);
            GameManager.instance.UIInventoryManager.DraggedItem.ItemStack -= 1;
            GameManager.instance.UIInventoryManager.DraggedItem.UpdateQuantity();

            if (GameManager.instance.UIInventoryManager.DraggedItem.ItemStack.Quantity == 0)
            {
                GameManager.instance.UIInventoryManager.ClearDraggedItem(true);
                return;
            }

        }
        else
        {
            // If only 1 left, move the dragged item opposed to duplicating.
            if (GameManager.instance.UIInventoryManager.DraggedItem.ItemStack.Quantity == 1)
            {
                if (GameManager.instance.UIInventoryManager.IsEquipmentSlot(inventorySlot))
                {
                    GameManager.instance.UIInventoryManager.EquipItemOnSlot((EquipmentSlotComponent) inventorySlot, GameManager.instance.UIInventoryManager.DraggedItem);
                    return;
                }

                GameManager.instance.UIInventoryManager.DropItemOntoSlot(inventorySlot, GameManager.instance.UIInventoryManager.DraggedItem);
                GameManager.instance.UIInventoryManager.ClearDraggedItem(false);
                return;
            }

            // Create new itemstack of dragged item type with quantity of 1.
            GameManager.instance.UIInventoryManager.SetSlot(inventorySlot.UIInventory, inventorySlot.Slot, new ItemStack(GameManager.instance.UIInventoryManager.DraggedItem.ItemStack.Item, 1));
            GameManager.instance.UIInventoryManager.DraggedItem.ItemStack -= 1;
            GameManager.instance.UIInventoryManager.DraggedItem.UpdateQuantity();
        }
       
    }
}
