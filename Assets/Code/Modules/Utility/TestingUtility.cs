using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestingUtility : MonoBehaviour
{

    public UIInventoryManager UIInventoryManager;

    public Inventory TestInventory;


    private void Awake()
    {
        TestInventory.AddItem(ItemDatabase.GameItems["CHAIN_HELMET"]);
        TestInventory.AddItem(ItemDatabase.GameItems["PLATE_BOOTS"]);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(PreferencesManager.GetKeybind(PreferencesManager.GameAction.MENU_ESC)))
            Application.Quit();

    }
}
