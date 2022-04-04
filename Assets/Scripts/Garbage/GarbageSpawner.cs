using System.Collections.Generic;
using UnityEngine;
using LDtkUnity;

public class GarbageSpawner : MonoBehaviour
{
    public static List<GarbageSpawnPoint> spawnPositions = new List<GarbageSpawnPoint>();

    public List<GameObject> garbagePrefabs = new List<GameObject>();

    private void Start()
    {
        SpawnAllGarbages();
    }

    public void Reset()
    {
        Garbage[] garbages = GameObject.FindObjectsOfType<Garbage>();
        foreach (Garbage g in garbages)
            Destroy(g);

        SpawnAllGarbages();
    }

    public void SpawnAllGarbages()
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
        GameObject newGarbage = Instantiate(prefab, pos.transform.position, Quaternion.identity, transform);

        newGarbage.GetComponent<Garbage>().coins = pos.GetComponent<LDtkFields>().GetInt("Coins");
    }
}
