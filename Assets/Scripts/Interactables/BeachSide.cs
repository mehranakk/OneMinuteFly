using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachSide : Interactable
{
    [SerializeField] private float minDist = 1, maxDist = 10;
    [SerializeField] private float maxVolume = 0.25f;

    private GameObject startAnimationPosGameObject;
    private void Awake()
    {
        base.Awake();
        startAnimationPosGameObject = transform.Find("StartAnimationPos").gameObject;
    }

    private void Start()
    {
        AudioManager.GetInstance().PlayByName("seaside", transform.position);
    }

    private void Update()
    {
        HandleAudioVolume();
    }

    private void OnEnable()
    {
        GameManager.GetInstance().OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.GetInstance().OnGameOver -= OnGameOver;
    }

    private void HandleAudioVolume()
    {
        Vector3 distance = GameManager.GetInstance().GetPlayer().transform.position - transform.position;
        float playerDistance = distance.magnitude;
        float audiblePercentage = Mathf.InverseLerp(maxDist, minDist, playerDistance);

        AudioManager.GetInstance().ChangeVolumeByName("seaside", maxVolume * audiblePercentage);
    }

    public override void Interact()
    {
        Debug.Log("Eat icecream at beachside");
        StartCoroutine(EatIceCream());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (InventoryController.GetInstance().hasIceCream)
            base.OnTriggerEnter2D(collision);
        else
            GameManager.GetInstance().GetPlayer().GetComponentInChildren<CharacterThinking>().Think(CharacterThinking.ThinkingEnum.ICE_CREAM);
    }

    private IEnumerator EatIceCream()
    {
        GameManager.GetInstance().PauseMainGame();
        CharacterMovement player = GameManager.GetInstance().GetPlayer().GetComponent<CharacterMovement>();
        player.EnableAnimationFly(startAnimationPosGameObject.transform.position, 0.1f);
        yield return new WaitForSeconds(0.5f);

        player.DisableAnimationFly();
        player.GetComponent<Animator>().SetTrigger("EatIceCream");

        AudioManager.GetInstance().PlayByName("ice-cream", transform.position);

        yield return new WaitForSeconds(1.3f);

        AudioManager.GetInstance().StopByName("ice-cream");

        InventoryController.GetInstance().UseIceCream();
        GameManager.GetInstance().UnpauseMainGame();
        TaskController.GetInstance().DoneTask(TaskController.TasksEnum.EAT_ICECREAM);
    }

    private void OnGameOver()
    {
        AudioManager.GetInstance().StopByName("seaside");
    }
}
