using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamSpawner: MonoBehaviour
{
    public static List<Vector2> spawnPoints = new List<Vector2>();

    [SerializeField] private GameObject iceCreamPrefab;

    public void Respawn()
    {
        foreach (Vector2 pos in spawnPoints)
            Instantiate(iceCreamPrefab, pos, Quaternion.identity);
    }
}
