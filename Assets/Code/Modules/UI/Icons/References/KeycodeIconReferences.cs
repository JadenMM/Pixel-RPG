using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Elements/References/KeyCodes")]
public class KeycodeIconReferences : SerializedScriptableObject
{
    public Dictionary<KeyCode, Sprite> KeyIcons;
}
