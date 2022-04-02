using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MateSpawnPoint : MonoBehaviour
{
    private void OnEnable()
    {
        MatingSystem.spawnPoints.Add(transform.position);
    }

    private void OnDisable()
    {
        MatingSystem.spawnPoints.Remove(transform.position);
    }
}
