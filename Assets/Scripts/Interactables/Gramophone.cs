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

    private void OnEnable()
    {
        GameManager.GetInstance().OnGameOver += OnGameOver;
        GameManager.GetInstance().OnPlayerDeath += OnPlayerDeath;
    }

    private void OnDisable()
    {
        GameManager.GetInstance().OnGameOver -= OnGameOver;
        GameManager.GetInstance().OnPlayerDeath -= OnPlayerDeath;
    }

    private void Start()
    {
    }

    public override void Interact()
    {
        StartCoroutine(PlayJazz());
    }

    private void Update()
    {
        HandleAudioVolume();
    }

    private IEnumerator PlayJazz()
    {
        gramophoneAnimator.SetBool("IsPlaying", true);
        AudioManager.GetInstance().PlayByName("jazz", transform.position);
        GameManager.GetInstance().PauseMainGame();
        GameManager.GetInstance().GetPlayer().GetComponent<CharacterMovement>().StartJazzDance();
        yield return new WaitForSeconds(5);
        gramophoneAnimator.SetBool("IsPlaying", false);
        GameManager.GetInstance().GetPlayer().GetComponent<CharacterMovement>().EndJazzDance();
        GameManager.GetInstance().UnpauseMainGame();
        TaskController.GetInstance().DoneTask(TaskController.TasksEnum.JAZZ);
    }

    private void HandleAudioVolume()
    {
        Vector3 distance = GameManager.GetInstance().GetPlayer().transform.position - transform.position;
        float playerDistance = distance.magnitude;
        float audiblePercentage = Mathf.InverseLerp(5, 1, playerDistance);

        AudioManager.GetInstance().ChangeVolumeByName("jazz", 0.6f * audiblePercentage);
    }

    public void OnPlayerDeath()
    {
        AudioManager.GetInstance().StopByName("jazz");
    }

    public void OnGameOver()
    {
        AudioManager.GetInstance().StopByName("jazz");
    }
}
