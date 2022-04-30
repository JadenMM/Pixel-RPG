using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public PlayerQuestData QuestData;
    public QuestTracker QuestTracker;

    private void Awake()
    {
        QuestData = new();
    }

    public bool IsEligibleForQuest(Quest quest)
    {
        var completedQuests = QuestData.Quests[QuestStatus.COMPLETE];
        
        // Checks if required objectives intersect with completed quests.
        bool preReq = quest.Requirements.Intersect(completedQuests).Count() == quest.Requirements.Count();

        // Check if player is already working on quest or has completed it
        bool questHistory = QuestData.Quests.Values.Where(l => l.Contains(quest)).Any();

        return preReq && !questHistory;

    }
}
