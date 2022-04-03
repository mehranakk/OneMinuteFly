using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mate : Interactable
{
    private Animator mateAnimator;

    private bool isFollowing = false;
    public bool alreadyMated = false; 
    private GameObject matingFlower;

    [SerializeField] private float followDistance = 2;
    [SerializeField] private float velocity = 4;

     private GameObject matingHeaert;
    private bool matingHeartHappend = false;

    protected void Awake()
    {
        base.Awake();
        mateAnimator = GetComponent<Animator>();
        matingHeaert = transform.Find("MatingHeart").gameObject;
    }

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
        GameManager.GetInstance().GetPlayer().GetComponent<CharacterMovement>().isInLove = false;
    }

    private void Update()
    {
        if (!isFollowing && matingFlower == null)
            return;
        else if (matingFlower != null)
        {
            Vector2 flowerHeadPos = matingFlower.transform.position;
            flowerHeadPos.y += 0.2f;
            if (Vector2.Distance(transform.position, flowerHeadPos) < 0.01)
            {
                InFlower();
            }
            else
            {
                GoToFlower();
            }
        }
        else
        {
            FollowPlayer();
        }

    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (!isInteractionLock)
            GameManager.GetInstance().GetPlayer().GetComponent<CharacterMovement>().isInLove = true;
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        if (!isInteractionLock)
            GameManager.GetInstance().GetPlayer().GetComponent<CharacterMovement>().isInLove = false;
    }

    private void FollowPlayer()
    {
        mateAnimator.SetBool("IsFlying", true);
        GameObject player = GameManager.GetInstance().GetPlayer();
        if (player.GetComponent<CharacterMovement>().IsDead())
            return;
        if (Vector2.Distance(transform.position, player.transform.position) < followDistance)
            return;
        Vector2 direction = (player.transform.position - transform.position);
        transform.Translate(direction * velocity * Time.deltaTime);
        if (direction.x > 0)
            Flip(1);
        else if (direction.x < 0)
            Flip(-1);
    }

    private void GoToFlower()
    {
        mateAnimator.SetBool("IsFlying", true);
        Vector2 direction = (matingFlower.transform.position - transform.position);
        direction.y += 0.2f;
        transform.Translate(direction * velocity * Time.deltaTime);
        if (direction.x > 0)
            Flip(1);
        else if (direction.x < 0)
            Flip(-1);
    }

    private void InFlower()
    {
        mateAnimator.SetBool("IsFlying", false);
        if (!matingHeartHappend)
        {
            Vector2 matingHeartPos = (transform.position + GameManager.GetInstance().GetPlayer().transform.position) / 2;
            matingHeartPos.y += 0.7f;
            matingHeaert.transform.position = matingHeartPos;
            StartCoroutine(Mating());
            matingHeartHappend = true;
        }
    }

    IEnumerator Mating()
    {
        matingHeaert.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        matingHeaert.SetActive(false);
        GameManager.GetInstance().GetPlayer().GetComponent<CharacterMovement>().EndMating();
        GameManager.GetInstance().UnpauseMainGame();
    }

    public void MateInFlower(GameObject flower)
    {
        matingFlower = flower;
        alreadyMated = true;

        mateAnimator.SetTrigger("Flower");
    }

    public void DestroySelf()
    {
        StartCoroutine(WaitAndDestroy());
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    private void Flip(int facing)
    {
        transform.localScale = new Vector3(facing, 1, 1);
    }
}
