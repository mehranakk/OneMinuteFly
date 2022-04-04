using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : Interactable
{
    private Animator fountainAnimator;
    private GameObject startAnimationPosGameObject;

    private void Awake()
    {
        base.Awake();
        fountainAnimator = GetComponent<Animator>();
        startAnimationPosGameObject = transform.Find("StartAnimationPos").gameObject;
    }

    public override void Interact()
    {
        Debug.Log("Dive in fountain");
        StartCoroutine(Dive());
    }

    private IEnumerator Dive()
    {
        GameManager.GetInstance().PauseMainGame();
        CharacterMovement player = GameManager.GetInstance().GetPlayer().GetComponent<CharacterMovement>();
        player.EnableAnimationFly(startAnimationPosGameObject.transform.position, 0.1f);
        yield return new WaitForSeconds(1f);

        player.DisableAnimationFly();
        player.HideCharacterSpriteRenderer();
        fountainAnimator.SetTrigger("Dive");

        AudioManager.GetInstance().PlayByName("dive", transform.position, delay: 1);

        yield return new WaitForSeconds(2f);

        player.UnhideCharacterSpriteRenderer();
        GameManager.GetInstance().UnpauseMainGame();
        TaskController.GetInstance().DoneTask(TaskController.TasksEnum.DIVE);
    }
}
