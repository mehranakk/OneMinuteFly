using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachSide : Interactable
{
    private GameObject startAnimationPosGameObject;
    private void Awake()
    {
        base.Awake();
        startAnimationPosGameObject = transform.Find("StartAnimationPos").gameObject;
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
    }

    private IEnumerator EatIceCream()
    {
        GameManager.GetInstance().PauseMainGame();
        CharacterMovement player = GameManager.GetInstance().GetPlayer().GetComponent<CharacterMovement>();
        player.EnableAnimationFly(startAnimationPosGameObject.transform.position, 0.1f);
        yield return new WaitForSeconds(0.5f);

        player.DisableAnimationFly();
        player.GetComponent<Animator>().SetTrigger("EatIceCream");

        yield return new WaitForSeconds(1.3f);

        InventoryController.GetInstance().UseIceCream();
        GameManager.GetInstance().UnpauseMainGame();
        TaskController.GetInstance().DoneTask(TaskController.TasksEnum.EAT_ICECREAM);
    }
}
