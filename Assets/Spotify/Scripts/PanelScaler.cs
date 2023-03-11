using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class PanelScaler : MonoBehaviour
{
    [SerializeField]
    private RoundedBoxProperties roundedBoxProperties;

    [SerializeField]
    private RectTransform canvasRect;

    private void Awake()
    {
        UpdatePanelScale();
    }

    public void UpdatePanelScale()
    {
        print("Updated panel scale");

        roundedBoxProperties.Width = canvasRect.rect.width * canvasRect.localScale.x;
        roundedBoxProperties.Height = canvasRect.rect.height * canvasRect.localScale.y;
    }
}
