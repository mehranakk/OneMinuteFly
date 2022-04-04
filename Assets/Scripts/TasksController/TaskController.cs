using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController
{
    public enum TasksEnum
    {
        MATE,
        DIVE,
        EAT_ICECREAM,
        PARTY,
        JAZZ

    }
    public delegate void TaskDoneEvent(TaskController.TasksEnum taskEnum);
    public event TaskDoneEvent OnTaskDone;

    private static TaskController instance;
    private Dictionary<TasksEnum, Task> tasks = new Dictionary<TasksEnum, Task>();

    private TaskController()
    {
    }

    public static TaskController GetInstance()
    {
        if (instance == null)
            instance = new TaskController();
        return instance;
    }

    public void Init()
    {
        tasks.Clear();
        tasks.Add(TasksEnum.MATE, new Task("Mate for the first time"));
        tasks.Add(TasksEnum.DIVE, new Task("Dive in a fountain"));
        tasks.Add(TasksEnum.EAT_ICECREAM, new Task("Eat ice poop at beach in a shadow"));
        tasks.Add(TasksEnum.PARTY, new Task("Party with cool kids"));
        tasks.Add(TasksEnum.JAZZ, new Task("Listen to Jazz"));
    }

    public void DoneTask(TasksEnum taskEnum)
    {
        Task t = tasks[taskEnum];
        if (t.IsTaskDone())
            return;

        t.Done();
        OnTaskDone?.Invoke(taskEnum);
    }

    public bool AreAllTasksDone()
    {
        foreach (KeyValuePair<TaskController.TasksEnum, Task> entry in this.tasks)
            if (!entry.Value.IsTaskDone())
                return false;
        return true;
    }

    public Dictionary<TasksEnum, Task> GetTasks()
    {
        //return this.tasks.Values.ToList();
        return this.tasks;
    }
}
