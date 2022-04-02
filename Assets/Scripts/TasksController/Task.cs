using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    public delegate void TaskDoneEvent();
    public event TaskDoneEvent OnTaskDone;

    string description;

    private bool isDone = false;

    public Task(string description)
    {
        this.description = description;
        this.isDone = false;
    }

    public void Done()
    {
        isDone = true;
        OnTaskDone?.Invoke();
    }

    public string GetDescription()
    {
        return description;
    }

    public bool IsTaskDone()
    {
        return isDone;
    }
}
