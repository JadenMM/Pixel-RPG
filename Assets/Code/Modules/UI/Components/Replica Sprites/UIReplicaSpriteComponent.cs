using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UIReplicaSpriteComponent : MonoBehaviour
{

    public Character Character;
    public AnimationController AnimationController;

    public void LoadSpriteData()
    {

        foreach (AnimatedSprite sprite in Character.AnimationController.AnimatedSprites)
        {
            var slot = sprite.Slot;

            AnimationController.AnimatedSprites.Where(x => x.Slot == slot).FirstOrDefault().LoadAnimationData(Character.AnimatedSpriteSlots[slot]);
        }

        AnimationController.CurrentAnimation = AnimationType.WALK_DOWN;
        AnimationController.SetIdleAnimation(true);
    }

}
