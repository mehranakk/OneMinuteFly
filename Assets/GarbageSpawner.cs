using System.Collections.Generic;
using UnityEngine;
using LDtkUnity;

public class GarbageSpawner : MonoBehaviour
{
    public static List<GarbageSpawnPoint> spawnPositions = new List<GarbageSpawnPoint>();

    public List<GameObject> garbagePrefabs = new List<GameObject>();

    private void Start()
    {
        foreach (GarbageSpawnPoint pos in spawnPositions)
            SpawnGarbage(pos);
    }

    public void SpawnGarbage(GarbageSpawnPoint pos)
    {
        GameObject prefab;
        int garbageType = pos.GetComponent<LDtkFields>().GetInt("Type");
        if (garbageType > -1)
            prefab = garbagePrefabs[garbageType];
        else
            prefab = garbagePrefabs[Random.Range(0, garbagePrefabs.Count)];
        Instantiate(prefab, pos.transform.position, Quaternion.identity, transform);
    }

    public GameObject FindSpawnPoint()
    {
        int randIndex = Random.Range(0, spawnPositions.Count);
        return spawnPositions[randIndex].gameObject;
    }
    
}
