using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class MatingSystem
{
    [SerializeField] private GameObject prefab;
    private MateSpawner spawner;
    public static List<Vector3> spawnPoints = new List<Vector3>();

    private Mate followingMate;
    private static MatingSystem instance;
    
    public static List<Mate> mates = new List<Mate>();
    public static List<Flower> flowers = new List<Flower>();

    private MatingSystem()
    {
    }

    public void Init()
    {
        spawner = GameObject.FindObjectOfType<MateSpawner>();
    }

    public static MatingSystem GetInstance()
    {
        if (instance == null)
            instance = new MatingSystem();
        return instance;
    }

    public void Reset()
    {
        foreach (Mate m in mates)
            if (!m.alreadyMated)
                m.UnlockInteraction();
    }

    public void SetFollowingMateAndUnlockFlowers(Mate mate)
    {
        followingMate = mate;
        Debug.Log("unlocking flowers");
        foreach (Flower f in flowers)
        {
            f.UnlockInteraction();
        }
            
        foreach (Mate m in mates)
            m.LockInteraction();

        // Setting Help Text
        GameObject helpText = GameManager.GetInstance().GetHelpText();
        helpText.GetComponent<TextMeshProUGUI>().text = "Go to a flower to mate";
        helpText.SetActive(true);
    }

    public bool IsMateFollowing()
    {
        if (followingMate != null)
            return true;
        return false;
    }

    public void DoMateInFlower(GameObject flower)
    {
        followingMate.GetComponent<Mate>().MateInFlower(flower);
        GameManager.GetInstance().SetCheckpointFlower(flower);

        followingMate.GetComponent<Mate>().DestroySelf();

        followingMate = null;
        foreach (Flower f in flowers)
            f.LockInteraction();

        GameObject helpText = GameManager.GetInstance().GetHelpText();
        helpText.SetActive(false);

        spawner.SpawnMate();
    }

}
