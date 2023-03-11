using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Oculus.Interaction;

public class ShootGun : MonoBehaviour
{
    [SerializeField]
    private Transform bulletSpawnPoint;
    [SerializeField]
    private TrailRenderer bulletTrail;
    [SerializeField]
    private TargetSpawner targetSpawner;
    [SerializeField]
    private float shootDelay = 0.02f;
    [SerializeField]
    private float explosionForce = 10f;
    [SerializeField]
    private UnityEvent onShoot;

    private float lastShootTime;
    private AudioSource audioSource;
    private bool gunIsGrabbed = false;
    private bool triggerIsPulled = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!gunIsGrabbed) return; // || lastShootTime + shootDelay > Time.time
        if (!triggerIsPulled && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.9f)
        {
            triggerIsPulled = true;
            Shoot();
        }
        else if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) < 0.5f) triggerIsPulled = false;
    }

    public void GrabGun()
    {
        gunIsGrabbed = true;
    }

    public void ReleaseGun()
    {
        gunIsGrabbed = false;
    }

    [ContextMenu("Shoot")]
    public void Shoot()
    {
        TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPoint.position, Quaternion.identity);
        if (Physics.Raycast(bulletSpawnPoint.position, transform.forward, out RaycastHit hit, float.MaxValue))
        {
            StartCoroutine(SpawnTrail(trail, hit.point));
            if (hit.transform.gameObject.layer == 3)
            {
                hit.transform.GetComponentInParent<Target>().Destroy(hit.point, explosionForce);
                targetSpawner.Targets.Remove(hit.transform.gameObject);
                while (targetSpawner.transform.childCount < 3) targetSpawner.SpawnNewTarget();
            }
        }
        else StartCoroutine(SpawnTrail(trail, transform.forward * 100f));

        lastShootTime = Time.time;
        audioSource.Play();
        onShoot.Invoke();
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 endPos)
    {
        float time = 0f;
        Vector3 startPosition = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, endPos, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }
        trail.transform.position = endPos;
        //Instantiate(impactVFX, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(trail.gameObject, trail.time);
    }
}
