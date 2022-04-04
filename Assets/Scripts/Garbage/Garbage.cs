using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : Interactable
{
    public int coins = 1;

    private void Awake()
    {
        base.Awake();
    }

    public override void Interact()
    {
        Debug.Log("Interact with garbage");
        if (coins > 0)
        {
            Debug.Log("Found a coin");
            InventoryController.GetInstance().PickUp(InventoryController.PickUpItemsEnum.COIN);
            coins -= 1;
        }

        if (coins == 0)
            LockInteraction();

    }
}
