using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    private static GameManager instance;
    private GameObject checkpointFlower;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        InitAll();
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public GameObject GetPlayer()
    {
        if (player == null)
            player = GameObject.Find("player");
        return player;
    }

    public void SetCheckpointFlower(GameObject flower)
    {
        checkpointFlower = flower;
    }

    private void InitAll()
    {
        MatingSystem.GetInstance().Init();
    }
}
