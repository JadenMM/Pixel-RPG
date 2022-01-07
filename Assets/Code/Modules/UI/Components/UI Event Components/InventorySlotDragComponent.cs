using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Component used to handle dragging of items from and to inventory slots within inventory UI.
 */
public class InventorySlotDragComponent : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{

    private InventorySlotComponent inventorySlot;

    private void Awake()
    {
        inventorySlot = gameObject.GetComponent<InventorySlotComponent>();

        if (inventorySlot == null)
            Debug.LogError("Error... inventory slot click component is not attached to an object with an inventory slot component.");
    }

    private bool DropResult = false;

    public void OnBeginDrag(PointerEventData eventData)
    {

        if (eventData.button != PointerEventData.InputButton.Left || GameManager.instance.UIInventoryManager.DraggedItem != null || inventorySlot.InventoryItem == null)
        {
            Debug.Log("Cancelled drag");
            eventData.Reset();
            return;
        }

        Debug.Log("Started dragging");

        GameManager.instance.UIInventoryManager.DraggedItem = inventorySlot.InventoryItem;
        GameManager.instance.UIInventoryManager.DraggedOriginSlot = inventorySlot;
        inventorySlot.InventoryItem = null;

        GameManager.instance.UIInventoryManager.DraggedItem.gameObject.transform.SetParent(GameManager.instance.UIInventoryManager.SortingSlot.transform);

        MoveIcon(eventData.position);
    }

    // Called every frame while item is being dragged.
    public void OnDrag(PointerEventData eventData)
    {
        if (GameManager.instance.UIInventoryManager.DraggedItem == null)
            return;

        MoveIcon(eventData.position);
    }

    public void OnDrop(InventorySlotComponent invSlot)
    {
        
        if (GameManager.instance.UIInventoryManager.DraggedItem == null)
            return;

        DropResult = true;
        Debug.Log($"Detected drop at {invSlot}");

        // Call UI inventory manager to handle dropping item.
        GameManager.instance.UIInventoryManager.DropItemOntoSlot(invSlot);
    }

    public void OnEquip(EquipmentSlotComponent equipmentSlot)
    {
        if (GameManager.instance.UIInventoryManager.DraggedItem == null)
            return;

        if (equipmentSlot.EquipmentSlot != GameManager.instance.UIInventoryManager.DraggedItem.ItemStack.Item.EquipmentSlot)
        {
            DropResult = false;
            return;
        }

        GameManager.instance.UIInventoryManager.EquipItemOnSlot(equipmentSlot);
        DropResult = true;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (GameManager.instance.UIInventoryManager.DraggedItem == null)
        {
            Debug.LogWarning("Ignored drag due to dragged item being null.");
            return;
        }
            

        if (eventData.button != PointerEventData.InputButton.Left)
        {
            Debug.Log("Resetting drag...");
            eventData.Reset();
            return;
        }

        Debug.Log("Stopped dragging");

        // Was item dropped outside inventory?
        if (!DropResult)
        {
            Debug.Log($"Dropped outside inventory. {name}");
            GameManager.instance.UIInventoryManager.ReturnDraggedItem();
        }

        DropResult = false;
        GameManager.instance.UIInventoryManager.ClearDraggedItem(false);
    }

    // Will move the icon with the cursor as the item is dragged.
    private void MoveIcon(Vector2 position)
    {
        if (GameManager.instance.UIInventoryManager.DraggedItem != null)
            GameManager.instance.UIInventoryManager.DraggedItem.gameObject.transform.position = position;
    }
}
