using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HoverChecker : MonoBehaviour
{
    [SerializeField]
    private Transform _leftHand;

    [SerializeField]
    private Transform _rightHand;

    [SerializeField]
    private UnityEvent _onHover;

    [SerializeField]
    private UnityEvent _onExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            _onHover.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            _onExit.Invoke();
        }
    }
}
