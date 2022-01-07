using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Elements/Character")]
public class NPCInformation : SerializedScriptableObject
{
    public string ID;
    public string Name;

    public Dictionary<Tuple<int, int>, Item> InventoryItems;
    public List<Item> EquippedItems = new List<Item>();


}