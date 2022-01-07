using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotComponent : MonoBehaviour, IDropHandler
{

    public UIInventory UIInventory;
    public InventoryItemComponent InventoryItem = null;
    public int Slot = 0;

    public void Initialize(int slot, UIInventory inventory)
    {
        Slot = slot;
        UIInventory = inventory;
    }

    // If this slot has an item dropped onto it, call InventorySlotDragComponent to handle the rest.
    public virtual void OnDrop(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Left)
            eventData.pointerDrag.GetComponent<InventorySlotDragComponent>().OnDrop(this);
    }
}
