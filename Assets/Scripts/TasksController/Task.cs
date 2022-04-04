using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{

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
    }

    public string GetDescription()
    {
        return this.description;
    }

    public bool IsTaskDone()
    {
        return isDone;
    }
}
