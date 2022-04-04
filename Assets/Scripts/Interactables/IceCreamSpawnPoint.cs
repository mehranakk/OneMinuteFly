using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamSpawnPoint : MonoBehaviour
{
    private void OnEnable()
    {
        IceCreamSpawner.spawnPoints.Add(this.transform.position);
    }

    private void OnDisable()
    {
        IceCreamSpawner.spawnPoints.Remove(this.transform.position);
    }
}
