using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlotComponent : InventorySlotComponent, IDropHandler
{

    [EnumPaging]
    public EquipmentSlot EquipmentSlot;

    public void Awake()
    {
        UIInventoryManager.EquipmentSlots.Add(EquipmentSlot, this);
    }

    public override void OnDrop(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Left)
            eventData.pointerDrag.GetComponent<InventorySlotDragComponent>().OnEquip(this);
    }



}
