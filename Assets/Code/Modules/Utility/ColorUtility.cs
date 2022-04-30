using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ColourUtility
{
    private const float COLOUR_CONVERTER = 1 / 255f;

    public static Color CreateColour(int r, int b, int g, int a = 255)
    {
        return new Color(r * COLOUR_CONVERTER, b * COLOUR_CONVERTER, g * COLOUR_CONVERTER, a * COLOUR_CONVERTER);
    }
}
