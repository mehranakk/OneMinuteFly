using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem
{
    public Interactable currentIntractableObject { private set; get; }
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
        Debug.Log(currentIntractableObject);
        if (currentIntractableObject != null && !currentIntractableObject.IsInteractionLock())
            currentIntractableObject.Interact();
    }
}
