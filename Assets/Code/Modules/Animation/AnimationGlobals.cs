using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum AnimationGroup
{
    WALK = 0,
    SWING = 4,
}

public enum AnimationType
{
    WALK_UP,
    WALK_LEFT,
    WALK_DOWN,
    WALK_RIGHT,
    SWING_UP,
    SWING_LEFT,
    SWING_DOWN,
    SWING_RIGHT,
}

[CreateAssetMenu(menuName = "Elements/References/AnimationGlobals")]
public class AnimationGlobals : SerializedScriptableObject
{
    public Dictionary<AnimationGroup, int> StartRanges;
    public Dictionary<AnimationGroup, int> FrameCounts;
    public Dictionary<AnimationGroup, float> AnimationLengths;
}
