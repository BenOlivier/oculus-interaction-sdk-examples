using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ColliderEventTrigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent colliderEnter;

    [SerializeField]
    private UnityEvent colliderExit;

    private Collider thisCollider;

    private void Start()
    {
        thisCollider = transform.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hand") colliderEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hand") colliderExit.Invoke();
    }

    public void Activated()
    {
        print("ACTIVATED");
    }

    public void Deactivated()
    {
        print("DEACTIVATED");
    }
}
