using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public enum AnimationType
{
    WALK_DOWN, 
    WALK_UP, 
    WALK_LEFT, 
    WALK_RIGHT, 
    SWING_DOWN,
    SWING_UP,
    SWING_LEFT,
    SWING_RIGHT,
}

public class AnimationController : MonoBehaviour
{

    public List<AnimatedSprite> AnimatedSprites;

    public AnimationType CurrentAnimation;
    private bool idle = false;

    public void StartAnimation(AnimationType animation)
    {
        if (CurrentAnimation == animation && !idle)
            return;

        idle = false;

        CurrentAnimation = animation;

        StopAllCoroutines();

        foreach(var sprite in AnimatedSprites)
        {
            StartCoroutine(Animate(sprite, animation));
        }
    }

    public void SetIdleAnimation(bool force = false)
    {
        if (idle && !force)
            return;

        idle = true;

        StopAllCoroutines();

        foreach(var sprite in AnimatedSprites)
        {
            sprite.SetFrame(CurrentAnimation, 0);
        }

    }

    private IEnumerator Animate(AnimatedSprite animatedSprite, AnimationType animation)
    {
        int currentFrame = -1;

        if (!animatedSprite.Animations.ContainsKey(animation))
        {
            yield break;
        }

        while (true)
        {
            currentFrame += 1;

            if (currentFrame != 0 && currentFrame == animatedSprite.Animations[animation].Frames.Count)
            {
                currentFrame = 0;
            }

            //Debug.Log($"[{animatedSprite.Slot}] SETTING FRAME: {currentFrame} WITH ANIMATION {animation}");
            animatedSprite.SetFrame(animation, currentFrame);
            yield return new WaitForSeconds(animatedSprite.Animations[animation].AnimationLength/animatedSprite.Animations[animation].Frames.Count);
        }

    }

}

