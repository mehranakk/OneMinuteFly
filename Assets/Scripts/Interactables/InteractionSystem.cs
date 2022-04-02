using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    private Interactable currentIntractableObject;

    private static InteractionSystem instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    public static InteractionSystem GetInstance()
    {
        if (instance != null)
            return instance;
        throw new System.Exception("InteractionSystem: Instance is null and got called");
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
        if (currentIntractableObject != null)
            currentIntractableObject.Interact();
    }
}
