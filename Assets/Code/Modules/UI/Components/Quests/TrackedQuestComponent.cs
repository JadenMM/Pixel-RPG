using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TrackedQuestComponent : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Objectives;
    public RectTransform RectTransform;

    public Quest Quest;

    private Vector2 startingSize;
    private Color startingColour;


    private void Awake()
    {
        startingSize = RectTransform.sizeDelta;
        startingColour = Title.color;
    }

    public void UpdateInfo()
    {
        Title.text = Quest.Name;      
        Title.color = startingColour;

        StringBuilder sb = new StringBuilder();

        int linesCount = 1;
        if (Quest.IsCompleted())
        {
            Title.color = ColourUtility.CreateColour(0, 255, 29);
            sb.Append($"Return to {Quest.QuestGiver.Name}");
        }
        else
        {
            foreach (QuestObjective objective in Quest.Objectives)
            {
                sb.Append(objective.ObjectiveName);
                string suffix = objective.Type switch
                {
                    ObjectiveType.ITEM => $"({objective.GetCurrentItemQuantity()}/{objective.Quantity})",
                    _ => $"({(objective.IsComplete ? "1" : "0")}/1",
                };
                sb.Append($" {suffix}");
                sb.Append("\n");
            }

            linesCount = Quest.Objectives.Count;
        }

        Objectives.text = sb.ToString();

        RectTransform.sizeDelta = new Vector2(startingSize.x, linesCount * startingSize.y);
    }
}
