using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageColorToggle : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private Color color1 = Color.white;

    [SerializeField]
    private Color color2 = Color.white;

    private bool isColor1 = true;

    [ContextMenu("Toggle")]
    public void ToggleColor()
    {
        if (isColor1)
        {
            image.color = color2;
            isColor1 = false;
        }
        else
        {
            image.color = color1;
            isColor1 = true;
        }
    }
}
