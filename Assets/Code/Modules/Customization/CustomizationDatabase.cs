using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/*
 * Loaded at all times during gameplay, used to access customization elements.
 * NOTE: Optimization potential. Only loaded during character customization.
 */
public class CustomizationDatabase : MonoBehaviour
{
    public static Dictionary<string, CustomizationElement> CustomizationElements;

    private void Awake()
    {
        CustomizationElements = new Dictionary<string, CustomizationElement>();

        foreach (var item in Resources.LoadAll<CustomizationElement>("Customization"))
        {
            CustomizationElements.Add(item.ID.ToUpper(), item);
        }

    }


}
