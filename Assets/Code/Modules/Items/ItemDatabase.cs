using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


/*
 * Loaded at all times during gameplay, used to access all game items;
 */
public class ItemDatabase : MonoBehaviour
{
    public static Dictionary<string, Item> GameItems;

    private void Awake()
    {
        GameItems = new Dictionary<string, Item>();

        foreach (var item in Resources.LoadAll<Item>("Items"))
        {
            GameItems.Add(item.ID.ToUpper(), item);
        }

    }


}

