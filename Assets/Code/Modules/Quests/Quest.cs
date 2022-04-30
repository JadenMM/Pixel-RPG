using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Elements/Quest")]
public class Quest : ScriptableObject
{

    public string ID;
    public string Name;

    [TextArea]
    public string Description;

    [TextArea]
    public string FinishedDescription;

    public NPCInformation QuestGiver;

    public List<Quest> Requirements;

    public List<QuestObjective> Objectives;

    public List<Item> ItemRewards;

    public void OnAccept()
    {
        foreach(QuestObjective objective in Objectives)
        {
            objective.EvaluateObjective();

            switch (objective.Type)
            {
                case ObjectiveType.ITEM:
                    GameManager.instance.QuestManager.QuestTracker.AddTrackedItem((Item)objective.Object, this);
                    break;
            }
        }

        GameManager.instance.UIManager.QuestTrackingPanel.AddTrackedQuest(this);
    }

    public void OnComplete()
    {
        if (!IsCompleted())
            return;

        foreach(QuestObjective objective in Objectives)
        {
            switch (objective.Type)
            {
                case ObjectiveType.ITEM:
                    GameManager.instance.Player.Inventory.RemoveItem((Item)objective.Object, objective.Quantity);
                    break;
            }
        }

        foreach (Item item in ItemRewards)
        {
            GameManager.instance.Player.Inventory.AddItem(item);
        }

        GameManager.instance.UIManager.QuestTrackingPanel.RemoveTrackedQuest(this);
        
    }

        public bool IsCompleted()
    {
        foreach(QuestObjective obj in Objectives)
        {
            if (!obj.IsComplete)
                return false;
        }

        return true;
    }

    public void CheckObjectives(ObjectiveType type)
    {
        foreach (QuestObjective obj in Objectives.Where(o => o.Type == type))
        {
            var completeBefore = obj.IsComplete;

            obj.EvaluateObjective();
        }

        GameManager.instance.UIManager.QuestTrackingPanel.UpdateTrackedQuest(this);

        if (IsCompleted())
            NPCDatabase.GetActiveNPC(QuestGiver).ChooseStatusIcon();
    }
}
