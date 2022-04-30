using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PreferencesManager;

public class UIManager : MonoBehaviour
{
    public Player Player;

    public RectTransform HealthBar;
    public TextMeshProUGUI HealthText;
    private Vector2 healthBarSize;

    public List<UIPanelComponent> Panels;

    public QuestGiverPanelComponent QuestGiverPanel;
    public QuestTrackingPanelComponent QuestTrackingPanel;

    public GameObject InputPromptGroup;
    public GameObject InputPromptPrefab;

    public TextMeshProUGUI OnScreenText;

    private Dictionary<KeyCode, UIInputPromptComponent> InputPrompts;

    private void Awake()
    {
        InputPrompts = new Dictionary<KeyCode, UIInputPromptComponent>();
    }

    void Start()
    {
        healthBarSize = HealthBar.sizeDelta;
    }

    private void Update()
    {

        if (!Input.anyKeyDown)
            return;

        // For each panel, check if keybind is pressed to toggle the panel.
        foreach (UIPanelComponent panel in Panels)
        {
            if (Input.GetKeyDown(GetKeybind(panel.GameAction)))
            {
                panel.TogglePanel();
            }
        }
    }

    private void LateUpdate()
    {
        // NOTE: Do we want to update display every time damage is taken or on late update?
        UpdateHealthDisplay();
    }

    private void UpdateHealthDisplay()
    {
        // Update size of mask relative to max health.
        HealthBar.sizeDelta = new Vector2(Player.Health / Player.MaxHealth * healthBarSize.x, healthBarSize.y);
        // Update positioning of health bar to move it left upon update.
        HealthBar.anchoredPosition = new Vector2((healthBarSize.x - HealthBar.sizeDelta.x) / -2, HealthBar.anchoredPosition.y);
        // Update hoverable healthbar text to display current health.
        HealthText.text = $"HP: {Player.Health:0} / {Player.MaxHealth:0}";
    }

    public void OpenQuestGiverPanel(NPC npc, Quest quest)
    {
        QuestGiverPanel.Show();
        QuestGiverPanel.UpdatePanel(npc, quest);

    }

    public void AddInputPrompt(KeyCode key, string text)
    {
        if (InputPrompts.ContainsKey(key))
            return;

        var promptObject = Instantiate(InputPromptPrefab, InputPromptGroup.transform);
        var promptComponent = promptObject.GetComponent<UIInputPromptComponent>();

        promptComponent.Image.sprite = IconDatabase.KeyIcons[key];
        promptComponent.Text.text = text;

        InputPrompts.Add(key, promptComponent);
    }

    public void RemoveInputPrompt(KeyCode key)
    {
        if (!InputPrompts.ContainsKey(key))
            return;

        Destroy(InputPrompts[key].gameObject);
        InputPrompts.Remove(key);
    }

    public void ShowScreenAnnouncement(string text)
    {
        OnScreenText.gameObject.SetActive(true);
        OnScreenText.text = text;
        OnScreenText.GetComponent<UIFadeOutTextComponent>().StartFade();
    }



}
