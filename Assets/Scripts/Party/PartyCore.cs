using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyCore : Interactable
{
    
    protected new void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        AudioManager.GetInstance().PlayByName("party", transform.position, delay: Random.Range(0, 2f));
    }

    private void Update()
    {
        HandleAudioVolume();
    }

    public override void Interact()
    {
        GameManager.GetInstance().GetPlayer().GetComponent<CharacterMovement>().StartParty();
        LockInteraction();
    }

    private void OnGameOver() => AudioManager.GetInstance().StopByName("party");
    private void OnPlayerDeath() {}

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

    private void HandleAudioVolume()
    {
        Vector3 distance = GameManager.GetInstance().GetPlayer().transform.position - transform.position;
        float playerDistance = distance.magnitude;
        float audiblePercentage = Mathf.InverseLerp(6, 1, playerDistance);

        AudioManager.GetInstance().ChangeVolumeByName("party", 0.5f * audiblePercentage);
    }
}
