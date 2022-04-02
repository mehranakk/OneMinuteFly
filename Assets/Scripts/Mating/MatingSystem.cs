using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
            Debug.Log("flower unlocked: " + f.gameObject.name);
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
        followingMate = null;
        foreach (Flower f in flowers)
            f.LockInteraction();

        GameObject helpText = GameManager.GetInstance().GetHelpText();
        helpText.SetActive(false);
    }
}
