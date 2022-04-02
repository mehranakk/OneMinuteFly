using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LDtkUnity;

public class Flower : Interactable
{
    private void Start()
    {
        try
        {
            GetComponent<SpriteRenderer>().color = GetComponent<LDtkFields>().GetColor("Color");
            transform.localScale *= GetComponent<LDtkFields>().GetInt("Size");
        }
        catch { }

        LockInteraction();
    }

    private void OnEnable()
    {
        MatingSystem.flowers.Add(this);        
    }

    private void OnDisable()
    {
        MatingSystem.flowers.Remove(this);
    }

    public override void Interact()
    {
        Debug.Log("Interact With flower");
        MatingSystem.GetInstance().DoMateInFlower(this.gameObject);
    }
}
