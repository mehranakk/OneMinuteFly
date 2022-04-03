using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : Button
{

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void OnClicked()
    {
        GameManager.GetInstance().StartNewGame();
    }
}
