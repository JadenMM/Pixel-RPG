using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public enum StatusIcon
{
    NONE,
    QUEST_AVAILABLE,
    QUEST_COMPLETE,
}

public class IconDatabase : SerializedMonoBehaviour
{
    public KeycodeIconReferences KeyIconReference;
    public StatusIconReferences StatusIconReference;

    public static Dictionary<KeyCode, Sprite> KeyIcons = new Dictionary<KeyCode, Sprite>();
    public static Dictionary<StatusIcon, Sprite> StatusIcons = new Dictionary<StatusIcon, Sprite>();

    private void Awake()
    {
        foreach (var icon in KeyIconReference.KeyIcons)
        {
            KeyIcons.Add(icon.Key, icon.Value);
        }

        foreach(var icon in StatusIconReference.StatusIcons)
        {
            StatusIcons.Add(icon.Key, icon.Value);
        }
    }

}
