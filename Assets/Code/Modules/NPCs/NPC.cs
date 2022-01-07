using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class NPC : Character
{

    public NPCInformation NPCInformation;
    public List<Quest> Quests;
    public GameObject StatusIcon;


    public override void OnAwake()
    {
        if (NPCInformation == null)
        {
            Debug.LogWarning($"Tried to initialize an NPC without any NPC information: {gameObject.name}.");
            Destroy(gameObject);
            return;
        }

        Quests = new List<Quest>();

        NPCDatabase.ActiveNPCs.Add(NPCInformation, this);

        int i = Enum.GetValues(typeof(AnimatedSpriteSlot)).Length;
        foreach (AnimatedSpriteSlot slot in Enum.GetValues(typeof(AnimatedSpriteSlot)))
        {
            i--;
            AnimationController.AnimatedSprites.Add(CreateAnimatedSpriteSlot(slot, i).GetComponent<AnimatedSprite>());
        }

        if (NPCInformation.InventoryItems != null)
        {

            foreach (var slot in NPCInformation.InventoryItems)
            {
                Inventory.SetSlot(slot.Key.Item1, new ItemStack(slot.Value, slot.Key.Item2));
            }
        }

        if (NPCInformation.EquippedItems != null)
        {
            foreach (var item in NPCInformation.EquippedItems)
            {
                SetEquippedSlot(item.EquipmentSlot, item);
            }
        }
    }

    public override void OnStart()
    {
        if (Quests.Count > 1)
        {
            StatusIcon.SetActive(true);
        }
    }

    private GameObject CreateAnimatedSpriteSlot(AnimatedSpriteSlot slot, int layer)
    {
        var obj = new GameObject(slot.ToString(), typeof(AnimatedSprite), typeof(SpriteRenderer));
        obj.transform.SetParent(gameObject.transform, false);
        obj.transform.SetAsFirstSibling();
        var animatedSprite = obj.GetComponent<AnimatedSprite>();
        var spriteRenderer = obj.GetComponent<SpriteRenderer>();
        animatedSprite.Slot = slot;
        animatedSprite.SpriteRenderer = spriteRenderer;
        spriteRenderer.sortingLayerName = "Character";
        spriteRenderer.sortingOrder = layer;

        return obj;
    }

}
