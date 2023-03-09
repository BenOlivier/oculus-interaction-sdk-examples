using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTime : MonoBehaviour
{
    [SerializeField] private AnimationCurve openCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private AnimationCurve closeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private float openDuration = 1.0f, closeDuration = 1.0f;

    [HideInInspector] public bool IsAnimating;
    private AnimationCurve animationCurve;
    private float startTime, duration;

    public void SetOpenAnimation()
    {
        animationCurve = openCurve;
        duration = openDuration;
        startTime = Time.time;
        IsAnimating = true;
    }

    public void SetCloseAnimation()
    {
        animationCurve = closeCurve;
        duration = closeDuration;
        startTime = Time.time;
        IsAnimating = true;
    }

    public float AnimationTimeValue()
    {
        float completion = (Time.time - startTime) / duration;
        if (completion >= 1.0f) IsAnimating = false;
        return animationCurve.Evaluate(completion);
    }
}
