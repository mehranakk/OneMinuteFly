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
    private Image checkbox;

    public void Init()
    {
        description = transform.Find("Description").GetComponent<TextMeshProUGUI>();
        checkbox = transform.Find("CheckBox").GetComponent<Image>();
    }

    public void SetTask(Task task)
    {
        this.task = task;
        this.task.OnTaskDone += OnTaskDone;
        description.text = this.task.GetDescription();
        if (this.task.IsTaskDone())
            checkbox.sprite = checkbox_checked;
        else
            checkbox.sprite = checkbox_empty;

    }

    private void OnTaskDone()
    {
        checkbox.sprite = checkbox_checked;
    }

    private void OnDestroy()
    {
        task.OnTaskDone -= OnTaskDone;
    }
}
