using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem
{
    private Interactable currentIntractableObject;
    private static InteractionSystem instance;

    private InteractionSystem()
    {
    }

    public static InteractionSystem GetInstance()
    {
        if (instance == null)
            instance = new InteractionSystem();
        return instance;
    }

    public void SetCurrentInteractableObject(Interactable interactable)
    {
        currentIntractableObject = interactable;
    }

    public void ClearCurrentInteractableObject()
    {
        currentIntractableObject = null;
    }

    public void EnterInteraction()
    {
        if (currentIntractableObject != null && !currentIntractableObject.IsInteractionLock())
            currentIntractableObject.Interact();
    }
}
