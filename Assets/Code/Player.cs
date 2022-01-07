using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Character
{

    public Dictionary<GameObject, Interactable> Interactables;

    public override void OnStart()
    {
        Interactables = new Dictionary<GameObject, Interactable>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(PreferencesManager.GetKeybind(PreferencesManager.GameAction.MENU_CONTAINER)))
        {
            if (GameManager.instance.UIInventoryManager.ContainerInventoryPanel.activeInHierarchy)
                GameManager.instance.UIInventoryManager.CloseContainer();
            else if (Interactables.Count > 0)
                GameManager.instance.UIInventoryManager.OpenContainer(FindChest().Inventory);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
            Interactables.Add(collision.gameObject, collision.GetComponent<Interactable>());
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (Interactables.ContainsKey(collision.gameObject))
            Interactables.Remove(collision.gameObject);
    }

    private ChestComponent FindChest()
    {

        var chest = Interactables.Where(x => x.Value.GetType() == typeof(ChestComponent)).FirstOrDefault();

        if (chest.Value != null)
            return (ChestComponent)chest.Value;
        else
            return null;

    }

}
