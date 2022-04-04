using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WishListUI : MonoBehaviour
{
    [SerializeField] private GameObject tasksUIPrefab;

    private GameObject scrollGameObject;
    private GameObject tasksStartingPosition;

    private Dictionary<TaskController.TasksEnum, TaskUI> tasksUI = new Dictionary<TaskController.TasksEnum, TaskUI>();

    private void Awake()
    {
        scrollGameObject = transform.Find("Scroll").gameObject;
        tasksStartingPosition = scrollGameObject.transform.Find("TasksStartingPosition").gameObject;
        scrollGameObject.SetActive(false);
        
    }

    private void Start()
    {
        CreateTasksUI();
        TaskController.GetInstance().OnTaskDone += OnTaskDone;
    }

    public void CreateTasksUI()
    {
        Vector2 currentPosition = tasksStartingPosition.transform.position;
        foreach (KeyValuePair<TaskController.TasksEnum, Task> entry in TaskController.GetInstance().GetTasks())
        {
        //foreach (Task t in TaskController.GetInstance().GetTasks())
            GameObject newTaskUI = Instantiate(tasksUIPrefab, currentPosition, Quaternion.identity, scrollGameObject.transform);
            newTaskUI.GetComponent<TaskUI>().Init();
            newTaskUI.GetComponent<TaskUI>().SetTask(entry.Value);
            currentPosition.y -= 0.7f;
            tasksUI.Add(entry.Key, newTaskUI.GetComponent<TaskUI>());
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ShowWishList();
            GameManager.GetInstance().PauseMainGame();
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            HideWishList();
            GameManager.GetInstance().UnpauseMainGame();
        }
    }


    private void OnTaskDone(TaskController.TasksEnum taskEnum)
    {
        
        StartCoroutine(PlayCheckingTask(tasksUI[taskEnum]));
    }

    public IEnumerator PlayCheckingTask(TaskUI taskUI)
    {
        GameManager.GetInstance().PauseMainGame();
        ShowWishList();
        yield return new WaitForSeconds(0.2f);
        taskUI.DoneTask();
        yield return new WaitForSeconds(2f);
        HideWishList();
        GameManager.GetInstance().UnpauseMainGame();
    }

    public void ShowWishList()
    {
        scrollGameObject.SetActive(true);
    }
    public void HideWishList()
    {
        scrollGameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        TaskController.GetInstance().OnTaskDone -= OnTaskDone;
    }
}
