using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable: MonoBehaviour
{
    protected GameObject messageGameObject;
    protected bool isInteractionLock;

    protected void Awake()
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

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("inja");
        if (!isInteractionLock)
        {
            messageGameObject.SetActive(true);
            InteractionSystem.GetInstance().SetCurrentInteractableObject(this);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("unja");
        if (!isInteractionLock)
        {
            messageGameObject.SetActive(false);
            InteractionSystem.GetInstance().ClearCurrentInteractableObject();
        }
    }
}
