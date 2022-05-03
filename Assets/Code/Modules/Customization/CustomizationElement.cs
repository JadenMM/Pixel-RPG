using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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

    public Texture2D SpriteSheet;

    [Button]
    public void AutoSortAnimation()
    {

        AnimationGlobals globals = (AnimationGlobals)AssetDatabase.LoadAssetAtPath("Assets/AnimationGlobals.asset", typeof(AnimationGlobals));

        if (globals is null)
        {
            Debug.LogError("Couldn't load animation globals");
            return;
        }

        var path = AssetDatabase.GetAssetPath(SpriteSheet);
        Sprite[] sprites = AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>().ToArray();

        Debug.Log(sprites.Length);

        AnimationData.Clear();

        foreach (AnimationGroup group in Enum.GetValues(typeof(AnimationGroup)))
        {
            var startRange = globals.StartRanges[group];

            for (int i = 0; i < 4; i++)
            {
                var animation = new Animation();
                animation.AnimationType = (AnimationType)group + i;
                animation.AnimationLength = globals.AnimationLengths[group];

                int frameCount = globals.FrameCounts[group];
                for (int f = 0; f < frameCount; f++)
                {
                    var index = startRange + (i * frameCount) + f;
                    animation.Frames.Add(sprites[index]);
                }
                AnimationData.Add(animation);
            }

        }
    }

    [TableList]
    public List<Animation> AnimationData;

    private static AnimatedSpriteSlot[] animationSlots = (AnimatedSpriteSlot[])Enum.GetValues(typeof(AnimatedSpriteSlot));
}
