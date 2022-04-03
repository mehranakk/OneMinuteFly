using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : Button
{
    private GameObject firstPageMenuGameObject;
    private GameObject currentPage;

    protected override void Awake()
    {
        base.Awake();
        firstPageMenuGameObject = transform.parent.parent.Find("FirstPage").gameObject;
        currentPage = transform.parent.gameObject;
    }
    protected override void OnClicked()
    {
        currentPage.SetActive(false);
        firstPageMenuGameObject.SetActive(true);
    }
}
