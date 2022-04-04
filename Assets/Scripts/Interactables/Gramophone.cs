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
        gramophoneAnimator.SetBool("IsPlaying", true);
        GameManager.GetInstance().PauseMainGame();
        StartCoroutine(WaitAndStopPlaying());
    }

    private IEnumerator WaitAndStopPlaying()
    {
        yield return new WaitForSeconds(3);
        gramophoneAnimator.SetBool("IsPlaying", false);
        GameManager.GetInstance().UnpauseMainGame();
        TaskController.GetInstance().DoneTask(TaskController.TasksEnum.JAZZ);
    }
}
