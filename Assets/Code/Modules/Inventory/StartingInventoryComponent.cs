using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

public class StartingInventoryComponent : SerializedMonoBehaviour
{
    public Character Character;
    public Dictionary<Tuple<int, int>, Item> InventoryItems;
    public List<Item> EquippedItems = new List<Item>();

    private void Start()
    {
        if (InventoryItems != null)
        {

            foreach (var slot in InventoryItems)
            {
                Character.Inventory.SetSlot(slot.Key.Item1, new ItemStack(slot.Value, slot.Key.Item2));
            }
        }

        if (EquippedItems != null)
        {
            foreach (var item in EquippedItems)
            {
                Character.SetEquippedSlot(item.EquipmentSlot, item);
            }
        }

    }

}
