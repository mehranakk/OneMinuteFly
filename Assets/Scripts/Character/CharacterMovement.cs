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
    public bool isInLove = false;
    public bool isInShock = false;

    private void Awake()
    {
        characterAnimator = GetComponent<Animator>();
        raycaster = transform.Find("Raycaster").GetComponent<RayCaster>();
    }

    void Update()
    {
        if (isDead || GameManager.GetInstance().IsGamePaused())
            return;

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

    private void HandleMove()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        Vector2 normalizedInput = new Vector2(horizontalInput, verticalInput).normalized;
        speed = normalizedInput * velocity;

        speed.x = (speed.x > 0 && isRightBlock) || (speed.x < 0 && isLeftBlock) ? 0 : speed.x;
        speed.y = (speed.y > 0 && isUpBlock) || (speed.y < 0 && isDownBlock) ? 0 : speed.y;

        if (horizontalInput != 0 || verticalInput != 0)
            TaskController.GetInstance().DoneTask(TaskController.TasksEnum.FLY);
    }

    public void Die()
    {
        Debug.Log("Player Died");
        characterAnimator.SetTrigger("Death");
        isDead = true;
        StartCoroutine(WaitAndDeactive());
    }

    IEnumerator WaitAndDeactive()
    {
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
}
