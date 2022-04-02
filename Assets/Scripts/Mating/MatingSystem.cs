using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatingSystem
{
    private Mate followingMate;
    private static MatingSystem instance;
    private Flower[] flowers;
    private Mate[] mates;

    private MatingSystem()
    {
    }

    public void Init()
    {
        flowers = GameObject.FindObjectsOfType<Flower>();
        mates = GameObject.FindObjectsOfType<Mate>();
    }

    public static MatingSystem GetInstance()
    {
        if (instance == null)
            instance = new MatingSystem();
        return instance;
    }

    public void SetFollowingMateAndUnlockFlowers(Mate mate)
    {
        followingMate = mate;
        foreach (Flower f in flowers)
            f.UnlockInteraction();
        foreach (Mate m in mates)
            m.LockInteraction();
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
        followingMate = null;
        foreach (Flower f in flowers)
            f.LockInteraction();
    }
}
