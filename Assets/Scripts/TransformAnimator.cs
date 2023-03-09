using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformAnimator : AnimationTime
{
    [HideInInspector] public enum transformComponent { position, rotation, scale };
    [HideInInspector] public Vector3 startValue, targetValue;
    public transformComponent transformToAnimate;
    public Vector3 openTarget;
    public Vector3 closedTarget;
    public bool startOpen;

    public virtual void Start()
    {
        switch (transformToAnimate)
        {
            case transformComponent.position:
                if(!startOpen) transform.localPosition = closedTarget;
                break;
            case transformComponent.rotation:
                if (!startOpen) transform.localRotation = Quaternion.Euler(closedTarget);
                break;
            case transformComponent.scale:
                if (!startOpen) transform.localScale = closedTarget;
                break;
        }
    }

    public virtual void Update()
    {
        if (IsAnimating)
        {
            switch (transformToAnimate)
            {
                case transformComponent.position:
                    transform.localPosition = animationValue();
                    break;
                case transformComponent.rotation:
                    transform.localRotation = Quaternion.Euler(animationValue());
                    break;
                case transformComponent.scale:
                    transform.localScale = animationValue();
                    break;
            }
        }
    }

    public Vector3 animationValue()
    {
        return Vector3.Lerp(startValue, targetValue, AnimationTimeValue());
    }

    public virtual void Open()
    {
        switch (transformToAnimate)
        {
            case transformComponent.position:
                startValue = Vector3.zero;
                break;
            case transformComponent.rotation:
                startValue = Vector3.zero;
                break;
            case transformComponent.scale:
                startValue = transform.localScale;
                break;
        }
        targetValue = openTarget;
        SetOpenAnimation();
    }

    public virtual void Close()
    {
        switch (transformToAnimate)
        {
            case transformComponent.position:
                startValue = transform.localPosition;
                break;
            case transformComponent.rotation:
                startValue = transform.localRotation.eulerAngles;
                break;
            case transformComponent.scale:
                startValue = transform.localScale;
                break;
        }
        targetValue = closedTarget;
        SetCloseAnimation();
    }
}
