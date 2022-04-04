using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private float horizontalInput, verticalInput;

    private Animator characterAnimator;
    private RayCaster raycaster;

    [SerializeField] private float velocity;
    private Vector2 speed;
    private int facing = 1;
    private bool isTurning = false;

    private bool isRightBlock, isLeftBlock, isUpBlock, isDownBlock;

    private bool isDead = false;
    public bool isReborning { private set; get; }
    public bool isInLove = false;
    public bool isInShock = false;

    private bool isMating = false;
    private GameObject matingFlower;

    private bool isInAnimationFly = false;
    private Vector2 animationFlyTarget;
    private float animationFlyDistance;


    private void Awake()
    {
        characterAnimator = GetComponent<Animator>();
        raycaster = transform.Find("Raycaster").GetComponent<RayCaster>();
        isReborning = true;
    }

    void Update()
    {
        if (isMating)
        {
            GoToFlowerForMating();
            return;
        }

        if (isInAnimationFly)
        {
            AnimationFly();
            return;
        }

        if (isDead || isReborning || GameManager.GetInstance().IsGamePaused())
        {
            speed = Vector2.zero;
            return;
        }

        raycaster.CastAll();
        SetDirectionBlocks();

        HandleMove();

        if (Input.GetKeyDown(KeyCode.F))
            InteractionSystem.GetInstance().EnterInteraction();


        if (isInLove)
            characterAnimator.SetTrigger("InLove");
        else if (isInShock)
            characterAnimator.SetTrigger("InShock");

        if (facing == 1 && speed.x < 0)
        {
            characterAnimator.SetTrigger("Turn");
            characterAnimator.ResetTrigger("TurnFlip");
            isTurning = true;
        }
        else if (facing == -1 && speed.x > 0)
        {
            characterAnimator.SetTrigger("TurnFlip");
            characterAnimator.ResetTrigger("Turn");
            isTurning = true;
        } 
        if (Vector2.SqrMagnitude(speed) > 0)
            characterAnimator.SetBool("IsFlying", true);
        else
            characterAnimator.SetBool("IsFlying", false);

        // We don't change facing on zero
        if (speed.x > 0)
            facing = 1;
        else if (speed.x < 0)
            facing = -1;

        /*if (!isTurning)
        {
            if (speed.x > 0)
                Flip(1);
            else if (speed.x < 0)
                Flip(-1);
        }*/
    }

    private void LateUpdate()
    {
        transform.Translate(speed * Time.deltaTime);
        AdjustSelf();
    }

    private void SetDirectionBlocks()
    {
        isRightBlock = isLeftBlock = isUpBlock = isDownBlock = false;
        foreach (RaySensor ray in raycaster.GetByTag("RIGHT"))
            if (ray.isContacted)
                isRightBlock = true;

        foreach (RaySensor ray in raycaster.GetByTag("LEFT"))
            if (ray.isContacted)
                isLeftBlock = true;

        foreach (RaySensor ray in raycaster.GetByTag("UP"))
            if (ray.isContacted)
                isUpBlock = true;

        foreach (RaySensor ray in raycaster.GetByTag("DOWN"))
            if (ray.isContacted)
                isDownBlock = true;
    }

    private void AdjustSelf()
    {
        Vector2 adjustment = MoveablePhysics.AdjustObject(raycaster);

        if (adjustment.x != 0f || adjustment.y != 0f)
            transform.Translate(adjustment);
    }

    private void HandleMove()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        Vector2 normalizedInput = new Vector2(horizontalInput, verticalInput).normalized;
        speed = normalizedInput * velocity;

        speed.x = (speed.x > 0 && isRightBlock) || (speed.x < 0 && isLeftBlock) ? 0 : speed.x;
        speed.y = (speed.y > 0 && isUpBlock) || (speed.y < 0 && isDownBlock) ? 0 : speed.y;
    }

    private void GoToFlowerForMating()
    {
        Vector2 targetPosition = matingFlower.transform.position;
        targetPosition.y += 0.2f;
        if (Vector2.Distance(transform.position, targetPosition) < 0.35)
        {
            speed = Vector2.zero;
            return;
        }
        speed = velocity * (targetPosition - (Vector2)transform.position).normalized;
    }

    public void SetMatingFlower(GameObject flower)
    {
        isMating = true;
        matingFlower = flower;
    }

    public void EndMating()
    {
        isMating = false;
        matingFlower = null;
    }

    public void StartParty()
    {
        characterAnimator.SetTrigger("Party");
        GameManager.GetInstance().PauseMainGame();
        StartCoroutine(EnoughParty());
    }

    IEnumerator EnoughParty()
    {
        yield return new WaitForSeconds(3);
        GameManager.GetInstance().UnpauseMainGame();
        TaskController.GetInstance().DoneTask(TaskController.TasksEnum.PARTY);
    }

    public void StartJazzDance()
    {
        characterAnimator.SetBool("JazzDancing", true);
    }

    public void EndJazzDance()
    {
        characterAnimator.SetBool("JazzDancing", false);
    }

    private void AnimationFly()
    {
        if (Vector2.Distance(transform.position, animationFlyTarget) < animationFlyDistance)
        {
            speed = Vector2.zero;
            return;
        }
        speed = velocity * (animationFlyTarget - (Vector2)transform.position).normalized;
    }

    public void EnableAnimationFly(Vector2 targetPos, float distance)
    {
        isInAnimationFly = true;
        animationFlyTarget = targetPos;
        animationFlyDistance = distance;
    }

    public void DisableAnimationFly()
    {
        isInAnimationFly = false;
        animationFlyTarget = Vector2.zero;
        animationFlyDistance = 0;
    }

    public void HideCharacterSpriteRenderer()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void UnhideCharacterSpriteRenderer()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Die()
    {
        Debug.Log("Player Died");
        characterAnimator.SetTrigger("Death");
        isDead = true;
        StartCoroutine(WaitAndDeactive());
        AudioManager.GetInstance().StopAll();
    }

    IEnumerator WaitAndDeactive()
    {
        speed = Vector2.zero;

        yield return new WaitForSeconds(2f);

        this.gameObject.SetActive(false);
    }

    public bool IsDead()
    {
        return isDead;
    }

    // Animation Event
    public void StartTurning()
    {
        isTurning = true;
    }

    // Animation Event
    public void EndTurning()
    {
        isTurning = false;
    }

    // Animation Event
    public void EndTurningFlip()
    {
        isTurning = false;
    }

    //Animation Event
    public void EndReborning()
    {
        isReborning = false;
        GameManager.GetInstance().UnpauseMainGame();
    }
}
