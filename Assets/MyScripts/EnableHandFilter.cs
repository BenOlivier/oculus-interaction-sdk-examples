using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.Input.Filter;

public class EnableHandFilter : MonoBehaviour
{
    [SerializeField] private HandFilter leftHandFilter, rightHandFilter;

    void Start()
    {
        leftHandFilter.enabled = true;
        rightHandFilter.enabled = true;
    }
}
