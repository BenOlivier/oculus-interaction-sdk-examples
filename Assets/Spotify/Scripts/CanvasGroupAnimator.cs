using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGroupAnimator : ValueAnimator
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private float openTarget, closeTarget;

    [SerializeField]
    private bool startOpen = false;

    private float startValue, targetValue;

    private void Start()
    {
        if (!startOpen) canvasGroup.alpha = closeTarget;
    }

    private void Update()
    {
        if (IsAnimating) canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, AnimationTimeValue());
    }

    public void Open()
    {
        startValue = canvasGroup.alpha;
        targetValue = openTarget;
        SetOpenAnimation();
    }

    public void Close()
    {
        startValue = canvasGroup.alpha;
        targetValue = closeTarget;
        SetCloseAnimation();
    }
}