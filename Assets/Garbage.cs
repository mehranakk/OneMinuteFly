using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : Interactable
{
    public int coins;

    private void Awake()
    {
        base.Awake();
    }

    public override void Interact()
    {
        Debug.Log("Interact with garbage");
    }
}
