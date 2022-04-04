using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gramophone : Interactable
{
    private Animator gramophoneAnimator;

    private void Awake()
    {
        base.Awake();
        gramophoneAnimator = GetComponent<Animator>();
    }
    public override void Interact()
    {
        StartCoroutine(PlayJazz());
    }

    private IEnumerator PlayJazz()
    {
        gramophoneAnimator.SetBool("IsPlaying", true);
        GameManager.GetInstance().PauseMainGame();
        GameManager.GetInstance().GetPlayer().GetComponent<CharacterMovement>().StartJazzDance();
        yield return new WaitForSeconds(5);
        gramophoneAnimator.SetBool("IsPlaying", false);
        GameManager.GetInstance().GetPlayer().GetComponent<CharacterMovement>().EndJazzDance();
        GameManager.GetInstance().UnpauseMainGame();
        TaskController.GetInstance().DoneTask(TaskController.TasksEnum.JAZZ);
    }
}
