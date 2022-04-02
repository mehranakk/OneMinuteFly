using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable: MonoBehaviour
{
    protected GameObject messageGameObject;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        messageGameObject.SetActive(true);
        InteractionSystem.GetInstance().SetCurrentInteractableObject(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        messageGameObject.SetActive(false);
        InteractionSystem.GetInstance().ClearCurrentInteractableObject();
    }
}
