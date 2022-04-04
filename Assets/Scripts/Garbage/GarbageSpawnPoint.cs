using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageSpawnPoint : MonoBehaviour
{

    private void OnEnable()
    {
        GarbageSpawner.spawnPositions.Add(this);
    }

    private void OnDisable()
    {
        GarbageSpawner.spawnPositions.Remove(this);
    }
}
