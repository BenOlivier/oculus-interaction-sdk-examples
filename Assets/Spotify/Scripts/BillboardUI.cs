using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    [SerializeField]
    private Transform hmdPosition;

    [SerializeField]
    private float lerpSpeed = 2f;

    void Update()
    {
        Quaternion targetRotation = Quaternion.LookRotation(transform.position - hmdPosition.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, lerpSpeed * Time.deltaTime);
    }
}
