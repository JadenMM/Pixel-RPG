using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class QuestTrackingPanelComponent : UIPanelComponent
{

    public Dictionary<Quest, TrackedQuestComponent> TrackedQuests = new();

    public GameObject TrackedQuestLayout;
    public GameObject TrackedQuestPrefab;

    public void AddTrackedQuest(Quest quest)
    {
        if (!IsOpen())
            Show();

        var trackedQuest = Instantiate(TrackedQuestPrefab, TrackedQuestLayout.transform);
        var trackedQuestComponent = trackedQuest.GetComponent<TrackedQuestComponent>();

        trackedQuestComponent.Quest = quest;

        TrackedQuests.Add(quest, trackedQuestComponent);
        trackedQuestComponent.UpdateInfo();
    }

    public void RemoveTrackedQuest(Quest quest)
    {
        if (!TrackedQuests.ContainsKey(quest))
            return;

        Destroy(TrackedQuests[quest].gameObject);
        TrackedQuests.Remove(quest);

        if (TrackedQuests.Count == 0)
            Hide();
    }

    public void UpdateTrackedQuest(Quest quest)
    {
        if (!TrackedQuests.ContainsKey(quest))
            return;

        TrackedQuests[quest].UpdateInfo();
    }

}