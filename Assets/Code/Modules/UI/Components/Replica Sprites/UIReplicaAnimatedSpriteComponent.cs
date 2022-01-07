using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIReplicaAnimatedSpriteComponent : AnimatedSprite
{

    public Image Image;

    public override void SetFrame(AnimationType anim, int id)
    {

        if (!Animations.ContainsKey(anim))
            return;

        if (Animations[anim].Frames.Count == 0)
            return;

        if (Animations[anim].Frames.Count <= id)
            return;

        var frame = Animations[anim].Frames[id];

        if (frame == null)
            return;

        Image.sprite = frame;
    }

    public override void LoadAnimationData(List<Animation> animations)
    {
        if (Animations.Count > 0)
            Animations.Clear();

        if (animations == null)
        {
            Image.enabled = false;
            return;
        }

        Image.enabled = true;

        foreach (Animation anim in animations)
        {
            Animations.Add(anim.AnimationType, anim);
        }
    }
}
