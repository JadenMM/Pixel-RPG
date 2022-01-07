using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PreferencesManager;

public class UIUpdateManager : MonoBehaviour
{
    public Player Player;

    public RectTransform HealthBar;
    public Text HealthText;
    private Vector2 healthBarSize;

    public List<UIPanelComponent> Panels;


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
        // Update size of health relative to max health.
        HealthBar.sizeDelta = new Vector2(Player.Health / Player.MaxHealth * healthBarSize.x, healthBarSize.y);
        // Update positioning of health bar to move it left upon update.
        HealthBar.anchoredPosition = new Vector2((healthBarSize.x - HealthBar.sizeDelta.x) / -2, HealthBar.anchoredPosition.y);
        // Update hoverable healthbar text to display current health.
        HealthText.text = $"{Player.Health} / {Player.MaxHealth} ({Player.Health / Player.MaxHealth * 100}%)";
    }




}
