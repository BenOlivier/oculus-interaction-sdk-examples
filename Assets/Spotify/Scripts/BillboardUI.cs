using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    [SerializeField]
    private Transform lookAtTransform;

    [SerializeField]
    private Transform hmdTransform;

    [SerializeField]
    private float lerpSpeed = 2f;

    [SerializeField]
    private Vector3 rotationMask = Vector3.one;

    void Update()
    {
        Vector3 lookAtRotation = Quaternion.LookRotation(lookAtTransform.position - hmdTransform.position).eulerAngles;
        Quaternion targetRotation = Quaternion.Euler(Vector3.Scale(lookAtRotation, rotationMask));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, lerpSpeed * Time.deltaTime);
    }
}
