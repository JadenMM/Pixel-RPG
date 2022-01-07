using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Elements/Customization Element")]
public class CustomizationElement : ScriptableObject
{

    protected const string LEFT_GROUP = "Split/Left";
    protected const string RIGHT_GROUP = "Split/Right";
    protected const string GENERAL_SETTINGS_VERTICAL_GROUP = "Split/Left/General Settings/Split/Right";

    public string ID;

    [ValueDropdown("animationSlots")]
    [LabelText("Anim Slot")]
    public AnimatedSpriteSlot AnimationSlot;

    [TableList]
    public List<Animation> AnimationData;

    private static AnimatedSpriteSlot[] animationSlots = (AnimatedSpriteSlot[])Enum.GetValues(typeof(AnimatedSpriteSlot));
}
