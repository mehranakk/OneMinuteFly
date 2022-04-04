using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyCore : Interactable
{
    
    protected new void Awake()
    {
        base.Awake();
    }

    public override void Interact()
    {
        GameManager.GetInstance().GetPlayer().GetComponent<CharacterMovement>().StartParty();
        LockInteraction();
    }
}
