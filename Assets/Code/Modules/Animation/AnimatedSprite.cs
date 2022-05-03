using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public enum AnimatedSpriteSlot
{
    MAIN_HAND,
    OFF_HAND,
    HANDS,
    HEAD,
    BELT,
    TORSO,
    LEGS,
    FEET,
    BODY,
    BEHIND
}

public class AnimatedSprite : MonoBehaviour
{

    public SpriteRenderer SpriteRenderer;
    public AnimatedSpriteSlot Slot;

    public Dictionary<AnimationType, Animation> Animations;

    private void Awake()
    {
        Animations = new Dictionary<AnimationType, Animation>();
    }

    public virtual void SetFrame(AnimationType anim, int id)
    {

        if (!Animations.ContainsKey(anim))
            return;
            
        if (Animations[anim].Frames.Count == 0)
            return;

        if (Animations[anim].Frames.Count <= id)
            return;

        var frame = Animations[anim].Frames[id];

        var colour = new Color(SpriteRenderer.color.r, SpriteRenderer.color.g, SpriteRenderer.color.b, 1);

        if (frame == null)
        {
            colour.a = 0;
        }

        SpriteRenderer.color = colour;
        SpriteRenderer.sprite = frame;
    }

    public virtual void LoadAnimationData(List<Animation> animations)
    {
        if (Animations.Count > 0)
            Animations.Clear();

        if (animations == null)
        {
            SpriteRenderer.sprite = null;
            return;
        }
            

        foreach(Animation anim in animations)
        {
            Animations.Add(anim.AnimationType, anim);
        }
    }
}

