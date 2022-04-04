using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController
{
    public enum TasksEnum
    {
        FLY,
        POOP,
        PARTY,

    }
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
        if (tasks.Count == 0)
        {
            tasks.Add(TasksEnum.FLY, new Task("Fly for the first time"));
            tasks.Add(TasksEnum.POOP, new Task("Poop somewhere nice"));
            tasks.Add(TasksEnum.PARTY, new Task("Party with cool kids"));
        }
    }

    public void DoneTask(TasksEnum taskEnum)
    {
        Task t = tasks[taskEnum];
        if (!t.IsTaskDone())
            t.Done();
    }

    public List<Task> GetTasks()
    {
        return this.tasks.Values.ToList();
    }
}
