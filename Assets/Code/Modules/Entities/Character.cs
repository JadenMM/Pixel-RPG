using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Character : MonoBehaviour
{

    public Inventory Inventory;
    public AnimationController AnimationController;
    public CombatController CombatController;

    public float Speed = 5f;
    public float Health = 100f;
    public float MaxHealth = 100f;

    public Dictionary<AnimatedSpriteSlot, List<Animation>> AnimatedSpriteSlots;

    private void Awake()
    {
        AnimatedSpriteSlots = new Dictionary<AnimatedSpriteSlot, List<Animation>>();

        foreach (AnimatedSpriteSlot slot in Enum.GetValues(typeof(AnimatedSpriteSlot)))
        {
            AnimatedSpriteSlots.Add(slot, null);
        }

        AnimatedSpriteSlots[AnimatedSpriteSlot.BODY] = CustomizationDatabase.CustomizationElements["BODY_MALE_WHITE"].AnimationData;

        OnAwake();
    }

    private void Start()
    {

        foreach(AnimatedSpriteSlot slot in AnimatedSpriteSlots.Keys)
        {
            if (AnimatedSpriteSlots[slot] == null)
                continue;

            AnimationController.AnimatedSprites.Where(x => x.Slot == slot).FirstOrDefault().LoadAnimationData(AnimatedSpriteSlots[slot]);
        }

        AnimationController.CurrentAnimation = AnimationType.WALK_DOWN;
        AnimationController.SetIdleAnimation();

        OnStart();
    }

    private void Update()
    {
        OnUpdate();
    }

    private void LateUpdate()
    {
        OnLateUpdate();
    }

    private void FixedUpdate()
    {
        OnFixedUpdate();
    }

    public void SetEquippedSlot(EquipmentSlot slot, Item item)
    {

        if (Inventory.Equipment.ContainsKey(slot) && Inventory.Equipment[slot] != null && item != null)
        {
            Inventory.AddItem(item);
        }

        AnimatedSprite sprite;

        if (item == null)
        {
            sprite = AnimationController.AnimatedSprites.Where(x => x.Slot == Inventory.Equipment[slot].AnimationSlot).FirstOrDefault();
            AnimatedSpriteSlots[Inventory.Equipment[slot].AnimationSlot] = null;
            Inventory.Equipment.Remove(slot);
            sprite.LoadAnimationData(null);
        }
        else
        {
            sprite = AnimationController.AnimatedSprites.Where(x => x.Slot == item.AnimationSlot).FirstOrDefault();
            AnimatedSpriteSlots[item.AnimationSlot] = item.AnimationData;
            Inventory.Equipment[slot] = item;
            sprite.LoadAnimationData(item.AnimationData);
        }

        sprite.SetFrame(AnimationController.CurrentAnimation, 0);
    }

    public virtual void OnAwake() { }
    public virtual void OnStart() {}
    public virtual void OnUpdate() { }
    public virtual void OnLateUpdate() { }
    public virtual void OnFixedUpdate() { }

}