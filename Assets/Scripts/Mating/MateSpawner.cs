using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MateSpawner : MonoBehaviour
{

    [SerializeField] private GameObject matePrefab;
    private Vector3 spawnPoint;

    public void SpawnMate()
    {
        // find a good spawn point within map
        spawnPoint = FindSpawnPoint();
        // spawn mate at spawn point
        Instantiate(matePrefab, spawnPoint, Quaternion.identity);
    }

    private Vector3 FindSpawnPoint()
    {
        int randIndex = Random.Range(0, MatingSystem.spawnPoints.Count);
        Debug.Log(randIndex);
        return MatingSystem.spawnPoints[randIndex];
    }
}
