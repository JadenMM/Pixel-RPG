using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Elements/Item")]
public class Item : ScriptableObject
{

    protected const string LEFT_GROUP                           = "Split/Left";
    protected const string RIGHT_GROUP                          = "Split/Right";
    protected const string GENERAL_SETTINGS_VERTICAL_GROUP      = "Split/Left/General Settings/Split/Right";

    [HideLabel, PreviewField(55)]
    [VerticalGroup(LEFT_GROUP)]
    [HorizontalGroup(LEFT_GROUP + "/General Settings/Split", 55, LabelWidth = 67)]
    public Sprite InventoryIcon;

    [BoxGroup(LEFT_GROUP + "/General Settings")]
    [VerticalGroup(GENERAL_SETTINGS_VERTICAL_GROUP)]
    public string ID;

    [BoxGroup(LEFT_GROUP + "/General Settings")]
    [VerticalGroup(GENERAL_SETTINGS_VERTICAL_GROUP)]
    public string Name;

    [BoxGroup(LEFT_GROUP + "/General Settings")]
    [VerticalGroup(GENERAL_SETTINGS_VERTICAL_GROUP)]
    [ValueDropdown("itemTypes")]
    public ItemType Type;

    [BoxGroup(LEFT_GROUP + "/General Settings")]
    [VerticalGroup(GENERAL_SETTINGS_VERTICAL_GROUP)]
    [ValueDropdown("equipmentSlots")]
    [ShowIf("Type", ItemType.EQUIPMENT)]
    [LabelText("Slot")]
    public EquipmentSlot EquipmentSlot;

    [BoxGroup(LEFT_GROUP + "/General Settings")]
    [VerticalGroup(GENERAL_SETTINGS_VERTICAL_GROUP)]
    [ValueDropdown("toolSlots")]
    [ShowIf("Type", ItemType.TOOL)]
    [LabelText("Slot")]
    public ToolSlot ToolSlot;

    [BoxGroup(LEFT_GROUP + "/General Settings")]
    [VerticalGroup(GENERAL_SETTINGS_VERTICAL_GROUP)]
    [ValueDropdown("animationSlots")]
    [ShowIf("Type", ItemType.EQUIPMENT)]
    [LabelText("Anim Slot")]
    public AnimatedSpriteSlot AnimationSlot;

    [VerticalGroup(RIGHT_GROUP)]
    [HorizontalGroup("Split", 0.5f, MarginLeft = 5, LabelWidth = 130)]
    [BoxGroup(RIGHT_GROUP + "/Description")]
    [HideLabel, TextArea(4, 9)]
    public string Description;

    [VerticalGroup(LEFT_GROUP)]
    [ShowIf("@this.Type == ItemType.EQUIPMENT || this.Type == ItemType.TOOL")]
    [BoxGroup(LEFT_GROUP + "/Animation")]
    [TableList]
    public List<Animation> AnimationData;

    private static ItemType[] itemTypes = (ItemType[]) Enum.GetValues(typeof(ItemType));
    private static EquipmentSlot[] equipmentSlots = (EquipmentSlot[]) Enum.GetValues(typeof(EquipmentSlot));
    private static ToolSlot[] toolSlots = (ToolSlot[]) Enum.GetValues(typeof(ToolSlot));
    private static AnimatedSpriteSlot[] animationSlots = (AnimatedSpriteSlot[]) Enum.GetValues(typeof(AnimatedSpriteSlot));

}
