using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MateSpawner : MonoBehaviour
{

    [SerializeField] private GameObject matePrefab;
    private Vector3 spawnPoint;

    public void SpawnMates()
    {
        foreach (Vector3 pos in MatingSystem.spawnPoints)
            Instantiate(matePrefab, pos, Quaternion.identity);
    }

    private Vector3 FindSpawnPoint()
    {
        int randIndex = Random.Range(0, MatingSystem.spawnPoints.Count);
        return MatingSystem.spawnPoints[randIndex];
    }
}
