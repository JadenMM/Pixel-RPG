using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    public List<AnimatedSprite> AnimatedSprites;

    public AnimationType CurrentAnimation;
    public AnimationGroup CurrentGroup;
    private bool idle = false;

    private int currentDirection = 2;

    public void StartAnimation(AnimationGroup group, int direction = -1, bool loop = true)
    {
        if (direction == -1)
            direction = currentDirection;

        currentDirection = direction;
        CurrentGroup = group;

        var newAnimation = (AnimationType) ((int) group + direction);

        if (CurrentAnimation == newAnimation && !idle)
            return;

        idle = false;

        CurrentAnimation = newAnimation;

        StopAllCoroutines();

        foreach(var sprite in AnimatedSprites)
        {
            StartCoroutine(Animate(sprite, CurrentAnimation, loop));
        }
    }

    public void SetIdleAnimation(bool force = false)
    {
        if (idle && !force || !IsMovementAnimation() && !force)
            return;

        idle = true;

        StopAllCoroutines();

        foreach(var sprite in AnimatedSprites)
        {
            sprite.SetFrame((AnimationType) currentDirection, 0);
        }

    }

    private IEnumerator Animate(AnimatedSprite animatedSprite, AnimationType animation, bool loop)
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
                if (loop)
                    currentFrame = 0;
                else
                    SetIdleAnimation(true);
            }

            Debug.Log($"[{animatedSprite.Slot}] SETTING FRAME: {currentFrame} WITH ANIMATION {animation}");
            animatedSprite.SetFrame(animation, currentFrame);
            yield return new WaitForSeconds(animatedSprite.Animations[animation].AnimationLength/animatedSprite.Animations[animation].Frames.Count);
        }

    }

    private bool IsMovementAnimation()
    {
        return CurrentAnimation is AnimationType.WALK_DOWN || CurrentAnimation is AnimationType.WALK_UP || CurrentAnimation is AnimationType.WALK_RIGHT || CurrentAnimation is AnimationType.WALK_LEFT;
    }

}

