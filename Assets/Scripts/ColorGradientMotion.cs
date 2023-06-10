using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorGradientMotion : MonoBehaviour
{
    public TMP_Text text;
    private TMP_ColorGradient colorGradient;

    Color topColor; 
    Color bottomColor;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        colorGradient = text.colorGradientPreset;

        ColorUtility.TryParseHtmlString("#002848", out topColor);
        ColorUtility.TryParseHtmlString("#BE00FF", out bottomColor);

        InvokeRepeating("SetColorGradient", .1f, .1f);
    }

    private int a = 0;
    private void SetColorGradient()
    {
        TMP_ColorGradient gradient = colorGradient;

        if (a == 0)
        {
            gradient.topLeft = topColor;
            gradient.topRight = topColor;

            gradient.bottomLeft = bottomColor;
            gradient.bottomRight = bottomColor;
        }
        else if (a == 1)
        {
            gradient.topLeft = bottomColor;
            gradient.topRight = bottomColor;

            gradient.bottomLeft = bottomColor;
            gradient.bottomRight = bottomColor;
        }
        else if (a == 2)
        {
            gradient.topLeft = bottomColor;
            gradient.topRight = bottomColor;

            gradient.bottomLeft = topColor;
            gradient.bottomRight = topColor;
        }
        else if (a == 3)
        {
            gradient.topLeft = topColor;
            gradient.topRight = topColor;

            gradient.bottomLeft = topColor;
            gradient.bottomRight = topColor;
            a = -1;
        }
        text.colorGradientPreset = gradient;
        a += 1;
    }

}
