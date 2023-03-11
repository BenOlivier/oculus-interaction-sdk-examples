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
    private float minLength = 0.02f;

    [SerializeField]
    private float maxLength = 0.04f;

    void Update()
    {
        float currentLength = Mathf.Lerp(minLength, maxLength, 0);

        Vector3[] points = new Vector3[2];
        points[0] = new Vector3(-currentLength, 0, 0);
        points[1] = new Vector3(currentLength, 0, 0);
        lineRenderer.SetPositions(points);
    }
}
