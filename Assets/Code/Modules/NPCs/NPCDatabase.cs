using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class NPCDatabase : MonoBehaviour
{

    public static Dictionary<string, NPCInformation> NPCs;

    public static Dictionary<NPCInformation, NPC> ActiveNPCs;



    private void Awake()
    {
        NPCs = new Dictionary<string, NPCInformation>();
        ActiveNPCs = new Dictionary<NPCInformation,NPC>();

        foreach (var npc in Resources.LoadAll<NPCInformation>("Characters"))
        {
            NPCs.Add(npc.ID.ToUpper(), npc);
        }

    }

    public static NPC GetActiveNPC(string ID)
    {
        return GetActiveNPC(NPCs[ID.ToUpper()]);
    }
    public static NPC GetActiveNPC(NPCInformation info)
    {
        return ActiveNPCs[info];
    }
}