using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public UIInventoryManager UIInventoryManager;
    public QuestManager QuestManager;
    public UIManager UIManager;
    public Player Player;

    public Texture2D CursorTexture;

    private void Awake()
    {
        instance = this;

        Application.targetFrameRate = 144;

        Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.Auto);
    }


}
