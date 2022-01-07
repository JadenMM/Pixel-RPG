using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
/*
 * TODO: Component to fill chests with random or static items.
 */
public class ChestFillerComponent : SerializedMonoBehaviour
{
    public ChestComponent Chest;
    public Dictionary<Tuple<int,int>, Item> InventoryItems = new Dictionary<Tuple<int,int>, Item>();

    private void Awake()
    {
        foreach(var slot in InventoryItems)
        {
            Chest.Inventory.SetSlot(slot.Key.Item1, new ItemStack(slot.Value, slot.Key.Item2));
        }
    }

}