using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WishListUI : MonoBehaviour
{
    [SerializeField] private GameObject tasksUIPrefab;

    private GameObject scrollGameObject;
    private GameObject tasksStartingPosition;

    private void Awake()
    {
        scrollGameObject = transform.Find("Scroll").gameObject;
        tasksStartingPosition = scrollGameObject.transform.Find("TasksStartingPosition").gameObject;
        scrollGameObject.SetActive(false);
        
    }

    private void Start()
    {
        CreateTasksUI();    
    }

    public void CreateTasksUI()
    {
        Vector2 currentPosition = tasksStartingPosition.transform.position;
        foreach (Task t in TaskController.GetInstance().GetTasks())
        {
            GameObject newTaskUI = Instantiate(tasksUIPrefab, currentPosition, Quaternion.identity, scrollGameObject.transform);
            newTaskUI.GetComponent<TaskUI>().Init();
            newTaskUI.GetComponent<TaskUI>().SetTask(t);
            currentPosition.y -= 0.4f;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            scrollGameObject.SetActive(true);
            GameManager.GetInstance().PauseMainGame();
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            scrollGameObject.SetActive(false);
            GameManager.GetInstance().UnpauseMainGame();
        }
    }
}
