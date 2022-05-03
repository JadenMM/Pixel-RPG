using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class Animation
{
    public AnimationType AnimationType;
    public List<Sprite> Frames;
    public float AnimationLength;

    public Animation()
    {
        Frames = new List<Sprite>();
    }
}
