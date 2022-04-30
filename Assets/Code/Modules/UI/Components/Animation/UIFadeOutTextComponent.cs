using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class UIFadeOutTextComponent : UIFadeOutComponent
{

    private TextMeshProUGUI text;

    public override void Awake()
    {
        SetColourDel = SetColour;

        text = gameObject.GetComponent<TextMeshProUGUI>();
        currentAlpha = text.color.a;
        startingAlpha = currentAlpha;

        fadeOutInterval = currentAlpha / FadeOut / 100;
    }

    private void SetColour(float manualSet = -1.0f)
    {

        if (manualSet >= 0)
            currentAlpha = manualSet;
        else
            currentAlpha -= fadeOutInterval;

        text.color = new Color(text.color.r, text.color.g, text.color.b, currentAlpha);
    }

}
