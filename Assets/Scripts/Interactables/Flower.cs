using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : Interactable
{
    private void Start()
    {
        LockInteraction();
    }

    public override void Interact()
    {
        Debug.Log("Interact With flower");
        MatingSystem.GetInstance().DoMateInFlower(this.gameObject);
    }
}
