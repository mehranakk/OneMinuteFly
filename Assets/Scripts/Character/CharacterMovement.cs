using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private float horizontalInput, verticalInput;

    private Animator characterAnimator;

    [SerializeField] private float velocity;
    private Vector2 speed;
    private int facing = 1;
    private int shouldFace = 1;
    private bool isTurning = false;

    private bool isDead = false;
    public bool isInLove = false;
    public bool isInShock = false;

    private void Awake()
    {
        characterAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead || GameManager.GetInstance().IsGamePaused())
            return;

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        speed = new Vector2(horizontalInput * velocity, verticalInput * velocity);

        if (horizontalInput != 0 || verticalInput != 0)
            TaskController.GetInstance().DoneTask(TaskController.TasksEnum.FLY);

        if (Input.GetKeyDown(KeyCode.F))
            InteractionSystem.GetInstance().EnterInteraction();

        transform.Translate(speed * Time.deltaTime);


        if (isInLove)
            characterAnimator.SetTrigger("InLove");
        else if (isInShock)
            characterAnimator.SetTrigger("InShock");

        if (facing == 1 && speed.x < 0)
        {
            characterAnimator.SetTrigger("Turn");
            isTurning = true;
        }
        else if (facing == -1 && speed.x > 0)
        {
            characterAnimator.SetTrigger("TurnFlip");
            isTurning = true;
        } 
        //if (!isTurning) { 
            if (Vector2.SqrMagnitude(speed) > 0)
                characterAnimator.SetBool("IsFlying", true);
            else
                characterAnimator.SetBool("IsFlying", false);
        //}

        // We don't change should facing on zero
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

    private void Flip(int facing)
    {
        transform.localScale = new Vector3(facing, 1, 1);
        this.facing = facing;
    }

    public void Die()
    {
        Debug.Log("Player Died");
        isDead = true;
        StartCoroutine(WaitAndDeactive());
    }

    IEnumerator WaitAndDeactive()
    {
        yield return new WaitForSeconds(1f);
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
