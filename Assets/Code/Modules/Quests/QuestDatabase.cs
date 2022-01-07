using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class QuestDatabase : MonoBehaviour
{

    public static Dictionary<string, Quest> Quests;

    private void Awake()
    {
        Quests = new Dictionary<string, Quest>();

        foreach (var quest in Resources.LoadAll<Quest>("Quests"))
        {
            Quests.Add(quest.ID.ToUpper(), quest);
        }

    }
}
