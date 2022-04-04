using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCream : Interactable
{
    private void Awake()
    {
        base.Awake();
    }

    public override void Interact()
    {
        Debug.Log("Pick up ice cream");
        InventoryController.GetInstance().PickUp(InventoryController.PickUpItemsEnum.ICE_CREAM);
        //LockInteraction();

        Destroy(this.gameObject);
    }
}
