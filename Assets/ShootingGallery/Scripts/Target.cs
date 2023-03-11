using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private Transform voxelsParent;
    [SerializeField]
    private float moveDistance = 0.2f;
    [SerializeField]
    private float moveDuration = 2f;
    [SerializeField]
    private float pauseDuration = 1f;
    [SerializeField]
    private AnimationCurve animationCurve;
    [SerializeField]
    private AudioSource audioSource;

    private float startTime;
    private Vector3 startPosition, newPosition;
    private bool isPausing = true;
    private bool isDestroyed = false;

    private void Awake()
    {
        startTime = Time.time;
        startPosition = transform.position;
    }

    [ContextMenu("Destroy")]
    public void Destroy(Vector3 hitPos, float force)
    {
        foreach (Transform voxel in voxelsParent)
        {
            Rigidbody rb = voxel.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddExplosionForce(force, hitPos, 1f);

            Destroy(voxel.gameObject, Random.Range(0f, 2f));
        }
        audioSource.Play();
        Destroy(gameObject, 2f);
        isDestroyed = true;
    }

    private void Update()
    {
        if (isDestroyed) return;
        if (isPausing)
        {
            if (startTime + pauseDuration > Time.time) return;
            isPausing = false;
            CalculateNewPosition();
        }
        else if (startTime + moveDuration > Time.time)
        {
            float time = animationCurve.Evaluate((Time.time - startTime) / moveDuration);
            transform.position = Vector3.Lerp(startPosition, newPosition, time);
        }
        else
        {
            isPausing = true;
            startTime = Time.time;
        }
    }

    private void CalculateNewPosition()
    {
        newPosition = Vector3.one * 100;
        while (Vector3.Distance(newPosition, transform.parent.position) > 2f || newPosition.y < 0.4f)
        {
            newPosition = transform.position + Random.insideUnitSphere.normalized * moveDistance;
        }
        startPosition = transform.position;
        startTime = Time.time;
    }
}
