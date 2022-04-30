using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class RPGElementsEditorWindow : OdinMenuEditorWindow
{

    [MenuItem("Design/RPG Elements/Editor")]
    private static void Open()
    {
        var window = GetWindow<RPGElementsEditorWindow>();
    }


    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree(true);

        tree.DefaultMenuStyle.IconSize = 28f;
        tree.Config.DrawSearchToolbar = true;

        tree.AddAllAssetsAtPath("Customization", "Assets/Resources/Customization", typeof(CustomizationElement), true);
        tree.AddAllAssetsAtPath("Items", "Assets/Resources/Items", typeof(Item), true);
        tree.AddAllAssetsAtPath("Quests", "Assets/Resources/Quests", typeof(Quest), true);
        

        return tree;
    }

    protected override void OnBeginDrawEditors()
    {
        var selected = MenuTree.Selection.FirstOrDefault();
        var toolbarHeight = MenuTree.Config.SearchToolbarHeight;

        // Draws a toolbar with the name of the currently selected menu item.
        SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
        {
            if (selected != null)
            {
                GUILayout.Label(selected.Name);
            }


            if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Element")))
            {
                var menu = new GenericMenu();

                menu.AddItem(new GUIContent("Item"), false, OnCreateSelected, "Item");
                menu.AddItem(new GUIContent("Customization"), false, OnCreateSelected, "Customization");
                menu.AddItem(new GUIContent("Quests"), false, OnCreateSelected, "Quest");

                menu.ShowAsContext();
            }

            /*if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Item")))
            {
                ScriptableObjectCreator.ShowDialog<Item>("Assets/Resources/Items", obj =>
                {
                    obj.ID = obj.name;
                    TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
                });
            }*/
        }
        SirenixEditorGUI.EndHorizontalToolbar();
    }

    private void OnCreateSelected(object tag)
    {

        if ((string) tag == "Item")
        {
            ScriptableObjectCreator.ShowDialog<Item>("Assets/Resources/Items", obj =>
            {
                obj.ID = obj.name;
                TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
            });
        }
        else if ((string) tag == "Customization")
        {
            ScriptableObjectCreator.ShowDialog<CustomizationElement>("Assets/Resources/Customization", obj =>
            {
                obj.ID = obj.name;
                TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
            });
        }
        else if ((string)tag == "Quest")
        {
            ScriptableObjectCreator.ShowDialog<CustomizationElement>("Assets/Resources/Quests", obj =>
            {
                obj.ID = obj.name;
                TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
            });
        }
    }
}