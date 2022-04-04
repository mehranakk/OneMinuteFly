using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LDtkUnity;

public class Flower : Interactable
{
    private Animator flowerAnimator;

    private void Awake()
    {
        base.Awake();
        flowerAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        try
        {
            //GetComponent<SpriteRenderer>().color = GetComponent<LDtkFields>().GetColor("Color");
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
        flowerAnimator.SetTrigger("MatedOn");
        AudioManager.GetInstance().PlayByName("powerup", transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsInteractionLock())
            base.OnTriggerEnter2D(collision);
        else if (!GameManager.GetInstance().GetPlayer().GetComponent<CharacterMovement>().isReborning)
            GameManager.GetInstance().GetPlayer().GetComponentInChildren<CharacterThinking>().Think(CharacterThinking.ThinkingEnum.MATE);
    }
}
