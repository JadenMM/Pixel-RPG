using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Elements/Quest")]
public class Quest : ScriptableObject
{

    public string ID;
    public string Name;
    public string Description;

    public NPCInformation QuestGiver;

    public List<Quest> Requirements;
    public List<Quest> Unlocks;
}
