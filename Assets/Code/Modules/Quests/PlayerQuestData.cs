using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum QuestStatus
{
    IN_PROGRESS, // When quest first starts
    COMPLETE, // When quest is completed and has been handed in
    FAILED // If quest has failed
}

[Serializable]
public class PlayerQuestData
{

    public Dictionary<QuestStatus, List<Quest>> Quests;

    public PlayerQuestData()
    {
        Quests = new Dictionary<QuestStatus, List<Quest>>();

        foreach(QuestStatus status in Enum.GetValues(typeof(QuestStatus)))
        {
            Quests[status] = new List<Quest>();
        }
    }

    public void CompleteQuest(Quest quest)
    {
        Quests[QuestStatus.IN_PROGRESS].Remove(quest);
        Quests[QuestStatus.COMPLETE].Add(quest);
        quest.OnComplete();

        GameManager.instance.UIManager.ShowScreenAnnouncement("Quest Completed");
        NPCDatabase.GetActiveNPC(quest.QuestGiver).ChooseStatusIcon();
    }

    public void FailQuest(Quest quest)
    {
        Quests[QuestStatus.IN_PROGRESS].Remove(quest);
        Quests[QuestStatus.FAILED].Add(quest);

        GameManager.instance.UIManager.ShowScreenAnnouncement("Quest Failed");
    }

    public void AbandonQuest(Quest quest)
    {
        Quests[QuestStatus.IN_PROGRESS].Remove(quest);
    }

    public void AcceptQuest(Quest quest)
    {
        Quests[QuestStatus.IN_PROGRESS].Add(quest);
        quest.OnAccept();

        GameManager.instance.UIManager.ShowScreenAnnouncement("Quest Started");
        NPCDatabase.GetActiveNPC(quest.QuestGiver).ChooseStatusIcon();
    }
}