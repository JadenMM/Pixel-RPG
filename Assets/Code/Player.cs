using Extensions;
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
        if (Input.GetKeyDown(PreferencesManager.GetKeybind(PreferencesManager.GameAction.OPEN_CONTAINER)))
        {
            if (GameManager.instance.UIInventoryManager.ContainerInventoryPanel.activeInHierarchy)
                GameManager.instance.UIInventoryManager.CloseContainer();
            else if (Interactables.Chests().Count > 0)
                GameManager.instance.UIInventoryManager.OpenContainer(FindChest().Inventory);
        }

        if (Input.GetKeyDown(PreferencesManager.GetKeybind(PreferencesManager.GameAction.INTERACT)))
        {
            if (GameManager.instance.UIManager.QuestGiverPanel.isActiveAndEnabled)
                GameManager.instance.UIManager.QuestGiverPanel.Hide();
            else if (Interactables.Characters().Count > 0)
                GameManager.instance.UIManager.OpenQuestGiverPanel(FindNPC().NPC, FindNPC().NPC.Quests[0]);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Detected trigger enter at: {collision.name}");

        if (collision.CompareTag("Interactable"))
        {
            var interactable = collision.GetComponent<Interactable>();
            GameManager.instance.UIManager.AddInputPrompt(PreferencesManager.GetKeybind(interactable.Action), interactable.InteractText);
            Interactables.Add(collision.gameObject, interactable);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log($"Detected trigger exit at: {collision.name}");

        if (Interactables.ContainsKey(collision.gameObject))
        {
            var interactableAction = Interactables[collision.gameObject].Action;
            Interactables.Remove(collision.gameObject);

            // Checks to make sure there aren't two interactables and we don't remove an input prompt that should still be applicable. (i.e. two chests)
            if (!Interactables.Values.Where(i => i.Action == interactableAction).Any())
                GameManager.instance.UIManager.RemoveInputPrompt(PreferencesManager.GetKeybind(interactableAction));
        }
    }

    private ChestComponent FindChest()
    {
        return Interactables.Chests()[0];
    }

    private InteractableCharacterComponent FindNPC()
    {
        return Interactables.Characters()[0];
    }

}
