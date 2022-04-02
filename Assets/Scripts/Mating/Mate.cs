using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mate : Interactable
{
    private bool isFollowing = false;
    public bool alreadyMated = false; 
    private GameObject matingFlower;
    [SerializeField] private float followDistance = 2;
    [SerializeField] private float velocity = 4;

    private void OnEnable()
    {
        MatingSystem.mates.Add(this);
    }

    private void OnDisable()
    {
        MatingSystem.mates.Remove(this);
    }

    public override void Interact()
    {
        Debug.Log("Interact With Mate");
        isFollowing = true;
        LockInteraction();
        MatingSystem.GetInstance().SetFollowingMateAndUnlockFlowers(this);
    }

    private void Update()
    {
        if (!isFollowing && matingFlower == null)
            return;
        else if (matingFlower != null)
        {
            GoToFlower();
        }
        else
        {
            FollowPlayer();
        }

    }

    private void FollowPlayer()
    {
        GameObject player = GameManager.GetInstance().GetPlayer();
        if (player.GetComponent<CharacterMovement>().IsDead())
            return;
        if (Vector2.Distance(transform.position, player.transform.position) < followDistance)
            return;
        Vector2 direction = (player.transform.position - transform.position);
        transform.Translate(direction * velocity * Time.deltaTime);
    }

    private void GoToFlower()
    {
        if (Vector2.Distance(transform.position, matingFlower.transform.position) < 0.01)
            return;
        Vector2 direction = (matingFlower.transform.position - transform.position);
        transform.Translate(direction * velocity * Time.deltaTime);
    }

    public void MateInFlower(GameObject flower)
    {
        matingFlower = flower;
        alreadyMated = true;
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
