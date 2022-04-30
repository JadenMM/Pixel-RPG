using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class QuestGiverPanelComponent : UIPanelComponent
{

    public TextMeshProUGUI NPCName;
    public TextMeshProUGUI QuestName;
    public TextMeshProUGUI QuestDescription;

    public GameObject AcceptQuestButton;
    public GameObject CompleteQuestButton;

    public GameObject RewardsGroup;
    public GameObject RewardsGrid;

    private Quest displayedQuest;

    public void UpdatePanel(NPC npc, Quest quest)
    {
        if (!IsOpen())
            return;

        displayedQuest = quest;

        NPCName.text = npc.NPCInformation.Name;
        QuestName.text = quest.Name;

        if (quest.IsCompleted())
            DisplayQuestComplete();
        else
            DisplayQuestInfo();
    }

    private void DisplayQuestInfo()
    {

        // Adds initial quest description + Quest Objectives
        StringBuilder sb = new StringBuilder();
        sb.Append(displayedQuest.Description);
        sb.Append("\n\nObjectives:");
        foreach (QuestObjective obj in displayedQuest.Objectives)
        {
            sb.Append("\n");
            sb.Append(obj.ObjectiveName + $" ({obj.Quantity})");
        }
        QuestDescription.text = sb.ToString();

        AcceptQuestButton.SetActive(true);
    }

    private void DisplayQuestComplete()
    {
        QuestDescription.text = displayedQuest.FinishedDescription;
        CompleteQuestButton.SetActive(true);

        // Display Rewards
        if (displayedQuest.ItemRewards.Count >= 1)
        {
            RewardsGroup.SetActive(true);
            foreach(Item item in displayedQuest.ItemRewards)
            {
                GameManager.instance.UIInventoryManager.CreateInventoryItemComponent(RewardsGrid.transform, new ItemStack(item, 1));
            }
        }

    }

    public void OnAcceptQuest()
    {
        GameManager.instance.QuestManager.QuestData.AcceptQuest(displayedQuest);
        Hide();
    }

    public void OnCompleteQuest()
    {
        GameManager.instance.QuestManager.QuestData.CompleteQuest(displayedQuest);
        Hide();
    }

    public void CleanPanel()
    {
        AcceptQuestButton.SetActive(false);
        CompleteQuestButton.SetActive(false);
        RewardsGroup.SetActive(false);
    }


}
