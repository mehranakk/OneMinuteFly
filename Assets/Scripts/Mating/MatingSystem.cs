using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class MatingSystem
{
    public delegate void HaveMatedEvent();
    public event HaveMatedEvent OnMate;

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
        Reset();
    }

    public static MatingSystem GetInstance()
    {
        if (instance == null)
            instance = new MatingSystem();
        return instance;
    }

    public void Reset()
    {
        for(int i = mates.Count - 1; i >=0; i--)
            mates[i].DestroySelf();

        RespawnAll();
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
        GameManager.GetInstance().PauseMainGame();
        followingMate.GetComponent<Mate>().MateInFlower(flower);
        GameManager.GetInstance().SetCheckpointFlower(flower);
        GameManager.GetInstance().GetPlayer().GetComponent<CharacterMovement>().SetMatingFlower(flower);

        OnMate?.Invoke();
        //followingMate.GetComponent<Mate>().DestroySelf();

        followingMate = null;
        foreach (Flower f in flowers)
            f.LockInteraction();

        GameObject helpText = GameManager.GetInstance().GetHelpText();
        helpText.SetActive(false);

    }

    public void RespawnAll()
    {
        spawner.SpawnMates();
    }

    public static void KillMatedMates()
    {
        foreach (Mate mate in mates)
            if (mate.alreadyMated)
                mate.DestroySelf();
    }

}
