using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public List<GameObject> Targets;

    [SerializeField]
    private GameObject targetPrefab;
    [SerializeField]
    private int startTargets = 3;
    [SerializeField]
    private float spawnRadius = 5f;

    [ContextMenu("Start")]
    public void SpawnStart()
    {
        StartCoroutine(SpawnStartTargets());
    }

    private IEnumerator SpawnStartTargets()
    {
        for (int i = 0; i < startTargets; i++)
        {
            SpawnNewTarget();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void SpawnNewTarget()
    {
        Vector3 spawnPos = transform.position + Random.insideUnitSphere * spawnRadius;
        GameObject targetInstance = Instantiate(targetPrefab, spawnPos, Quaternion.identity, transform);
        Targets.Add(targetInstance);
    }

    public void DestroyAllTargets()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Target>().Destroy(child.position, 1f);
        }
    }
}
