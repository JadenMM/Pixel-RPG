using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class QuestListExtensions
    {
        public static List<Quest> EligibleQuests(this List<Quest> quests)
        {
            var newQuests = new List<Quest>();

            foreach (var quest in quests)
            {
                if (GameManager.instance.QuestManager.IsEligibleForQuest(quest))
                    newQuests.Add(quest);
            }

            return newQuests;
        }

        public static List<Quest> ActiveQuestsWithNPC(this List<Quest> quests, NPCInformation npc)
        {
            var newQuests = new List<Quest>();

            foreach(var quest in quests)
            {
                if (quest.QuestGiver == npc)
                    newQuests.Add(quest);
            }

            return newQuests;

        }

        public static List<Quest> CompletedQuests(this List<Quest> quests)
        {
            var newQuests = new List<Quest>();

            foreach(var quest in quests)
            {
                if (quest.IsCompleted())
                    newQuests.Add(quest);
            }

            return newQuests;
        }
    }
}
