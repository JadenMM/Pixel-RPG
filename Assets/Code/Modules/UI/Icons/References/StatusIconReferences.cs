using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Elements/References/StatusIcons")]
public class StatusIconReferences : SerializedScriptableObject
{
    public Dictionary<StatusIcon, Sprite> StatusIcons;
}
