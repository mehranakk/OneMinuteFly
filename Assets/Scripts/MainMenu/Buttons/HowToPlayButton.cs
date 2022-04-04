using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayButton : Button
{
    private GameObject firstPageMenuGameObject;
    private GameObject creditsPageMenuGameObject;

    protected override void Awake()
    {
        base.Awake();
        firstPageMenuGameObject = transform.parent.gameObject;
        creditsPageMenuGameObject = transform.parent.parent.Find("HowToPlayPage").gameObject;
    }
    protected override void OnClicked()
    {
        firstPageMenuGameObject.SetActive(false);
        creditsPageMenuGameObject.SetActive(true);
    }
}
