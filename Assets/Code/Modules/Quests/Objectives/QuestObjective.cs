using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public enum ObjectiveType
{
    SPEAK, ITEM
}

[Serializable]
public class QuestObjective
{

    public string ObjectiveName;
    public ObjectiveType Type;
    public ScriptableObject Object;
    public int Quantity;

    [NonSerialized]
    public bool IsComplete;

    public bool EvaluateObjective()
    {

        var complete = Type switch
        {
            ObjectiveType.ITEM => GameManager.instance.Player.Inventory.HasItem(Object as Item, Quantity),
            _ => false,
        };

        if (complete)
            IsComplete = true;
        else
            IsComplete = false;

        return complete;
    }

    public int GetCurrentItemQuantity()
    {
        return GameManager.instance.Player.Inventory.GetItemAmount(Object as Item);
    }
}
