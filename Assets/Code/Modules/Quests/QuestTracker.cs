using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class QuestTracker : MonoBehaviour
{

    public Dictionary<Item, List<Quest>> ItemQuests = new Dictionary<Item, List<Quest>>();

    public void AddTrackedItem(Item item, Quest quest)
    {

        if (ItemQuests.ContainsKey(item))
        {
            ItemQuests[item].Add(quest);
        }
        else
        {
            var newList = new List<Quest>();
            newList.Add(quest);
            ItemQuests.Add(item, newList);
            
        }
    }

    public void CheckTrackedItem(Item item)
    {
        if (ItemQuests.ContainsKey(item))
        {
            foreach(Quest quest in ItemQuests[item])
            {
                quest.CheckObjectives(ObjectiveType.ITEM);
            }
        }
    }

    public void RemoveTrackedQuestItem(Item item, Quest quest)
    {
        if (!ItemQuests.ContainsKey(item))
            return;

        ItemQuests[item].Remove(quest);

        if (ItemQuests[item].Count <= 0)
            ItemQuests.Remove(item);
    }


}