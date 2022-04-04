using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskUI: MonoBehaviour
{
    private Task task;
    [SerializeField] private Sprite checkbox_empty;
    [SerializeField] private Sprite checkbox_checked;

    private TextMeshProUGUI description;
    private GameObject checkbox;

    public void Init()
    {
        description = transform.Find("Description").GetComponent<TextMeshProUGUI>();
        checkbox = transform.Find("CheckBox").gameObject;
    }

    public void SetTask(Task task)
    {
        //this.task = task;
        //this.task.OnTaskDone += OnTaskDone;
        description.text = task.GetDescription();
        if (task.IsTaskDone())
        {
            checkbox.GetComponent<Animator>().enabled = false;
            checkbox.GetComponent<SpriteRenderer>().sprite = checkbox_checked;
        }
        else
            checkbox.GetComponent<SpriteRenderer>().sprite = checkbox_empty;

    }

    public void DoneTask()
    {
        checkbox.GetComponent<Animator>().SetTrigger("Check");
        //checkbox.GetComponent<Animation>().Play("CheckboxChecking");
        StartCoroutine(WaitAndDisableAnimator());
    }

    private IEnumerator WaitAndDisableAnimator()
    {
        yield return new WaitForSeconds(2f);
        checkbox.GetComponent<Animator>().enabled = false;
        checkbox.GetComponent<SpriteRenderer>().sprite = checkbox_checked;
    }

    private void OnTaskDone()
    {
        checkbox.GetComponent<SpriteRenderer>().sprite = checkbox_checked;
    }

    private void OnDestroy()
    {
        //task.OnTaskDone -= OnTaskDone;
    }
}
