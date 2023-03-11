using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class GrabHandle : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [SerializeField]
    private float minLength = 0.02f;

    [SerializeField]
    private float maxLength = 0.04f;

    [SerializeField]
    private float animationDuration = 0.1f;

    private float startTime;

    private float startValue, targetValue;

    private bool isAnimating = false;

    private void Update()
    {
        if (isAnimating)
        {
            float time = animationCurve.Evaluate((Time.time - startTime) / animationDuration);
            if (time >= 1f) isAnimating = false;
            float lengthValue = Mathf.Lerp(startValue, targetValue, time);

            Vector3[] points = new Vector3[2];
            points[0] = new Vector3(-lengthValue, 0, 0);
            points[1] = new Vector3(lengthValue, 0, 0);
            lineRenderer.SetPositions(points);
        }
    }

    [ContextMenu("Expand")]
    public void ExpandLine()
    {
        startTime = Time.time;
        startValue = lineRenderer.GetPosition(1).x;
        targetValue = maxLength;
        isAnimating = true;
    }
    [ContextMenu("Collapse")]
    public void CollapseLine()
    {
        startTime = Time.time;
        startValue = lineRenderer.GetPosition(1).x;
        targetValue = minLength;
        isAnimating = true;
    }
}
