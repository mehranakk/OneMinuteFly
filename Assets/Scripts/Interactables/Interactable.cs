using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable: MonoBehaviour
{
    protected GameObject messageGameObject;
    protected bool isInteractionLock;

    private void Awake()
    {
        messageGameObject = transform.Find("InteractMessage").gameObject;
        if (messageGameObject.activeSelf)
            messageGameObject.SetActive(false);
    }

    public virtual void Interact()
    {
        throw new NotImplementedException("Must be implemented in child");
    }

    public void LockInteraction()
    {
        isInteractionLock = true;
        messageGameObject.SetActive(false);
    }

    public void UnlockInteraction()
    {
        isInteractionLock = false;
    }

    public bool IsInteractionLock()
    {
        return this.isInteractionLock;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isInteractionLock)
        {
            messageGameObject.SetActive(true);
            InteractionSystem.GetInstance().SetCurrentInteractableObject(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isInteractionLock)
        {
            messageGameObject.SetActive(false);
            InteractionSystem.GetInstance().ClearCurrentInteractableObject();
        }
    }
}
