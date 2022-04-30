using System;
using System.Collections.Generic;
using UnityEngine;

public class PreferencesManager : MonoBehaviour
{

    private static Dictionary<GameAction, KeyCode> keybinds;


    private void Awake()
    {
        keybinds = new Dictionary<GameAction, KeyCode>();

        LoadPreferences();
    }

    // Actions that take place in-game that should have a corresponding keybind.
    public enum GameAction
    {
        FORWARD = KeyCode.W,
        BACK = KeyCode.S,
        LEFT = KeyCode.A,
        RIGHT = KeyCode.D,
        MENU_INVENTORY = KeyCode.E,
        MENU_ESC = KeyCode.Escape,
        OPEN_CONTAINER = KeyCode.Q,
        INTERACT = KeyCode.F,
        NONE,
    }

    public void LoadPreferences()
    {

        // Loops through every keybind and load it into Keybinds dictionary.
        foreach (GameAction action in Enum.GetValues(typeof(GameAction)))
        {
            // If preferences is missing a key, add it to local storage first.
            if (!PlayerPrefs.HasKey(action.ToString()))
            {
                PlayerPrefs.SetInt(action.ToString(), (int)action);
                keybinds.Add(action, (KeyCode)action);
            }
            else
            {
                keybinds.Add(action, (KeyCode)PlayerPrefs.GetInt(action.ToString()));
            }
        }

        PlayerPrefs.Save();
    }

    // Returns corresponding loaded keybind for relative GameAction.
    public static KeyCode GetKeybind(GameAction action)
    {
        return keybinds[action];
    }

}
